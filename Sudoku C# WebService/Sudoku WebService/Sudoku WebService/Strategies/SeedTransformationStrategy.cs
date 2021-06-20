using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json.Linq;

using Sudoku_WebService.DataAccess;
using Sudoku_WebService.Models;

namespace Sudoku_WebService.Strategies
{
    public class SeedTransformationStrategy : IDisposable
    {
        // ASCII 97 is 'a'
        private const int CharOffset = 97;

        IConfiguration Config;

        //private int dimension;
        //private List<int> Tokens = new List<int>();
        //private Dictionary<int, int?> seedBoard;

        public SeedTransformationStrategy(IConfiguration config)
        {
            Config = config;
        }
        
        public async Task<GameModel> GeneratePuzzleFromSeed(Guid userId, string difficulty, int dimension, CancellationToken cancellationToken)
        {
            var seedBoard = new Dictionary<int, char>();
            var board = new Dictionary<int, NodeModel>();
            // Load starting seed
            var Seed = await LoadSeed(difficulty, dimension, cancellationToken);

            // Execute Token Shuffle
            var TransformTokens = ShuffleTokens(InitializeTokens(dimension));
            board = ApplyTokens(Seed.Board, TransformTokens);

            // Determine Transformations
            var Rand = new Random();
            var rotations = Rand.Next(0, 4);
            var flipHoriz = Rand.Next(0, 2);
            var flipVert = Rand.Next(0, 2);

            // Execute Transformations
            while (rotations > 0)
            {
                board = RotateBoard(board, dimension);
                rotations = rotations - 1;
            }
            while (flipHoriz > 0)
            {
                board = FlipBoardHoriz(board, dimension);
                flipHoriz = flipHoriz - 1;
            }
            while (flipVert > 0)
            {
                board = FlipBoardVert(board, dimension);
                flipVert = flipVert - 1;
            }

            var gameBoard = new GameModel()
            {
                Board = board,
                CompletedMoves = new Dictionary<int, MoveModel>(),
                PlayerId = userId
            };

            return gameBoard;
        }

        private List<char> InitializeTokens(int dimension)
        {
            var Tokens = new List<char>();

            for (int index = CharOffset; index < (CharOffset + dimension); index++)
            {
                Tokens.Add((char)index);
            }

            return Tokens;
        }
        /// <summary>
        /// Shuffle the tokens to use to swap values in the seed
        /// </summary>
        private List<char> ShuffleTokens(List<char> Tokens)
        {
            var ShuffledTokens = Tokens.ConvertAll(character => character).ToList();

            Tokens.CopyTo(ShuffledTokens.ToArray());

            // We skip swapping at index zero as there are no valid targets to swap with.
            for(int index = Tokens.Count - 1; index > 0; index--)
            {
                var Rand = new Random();
                var targetIndex = Rand.Next(0, index);

                // Swap the values between current index and target index
                var TokenHolder = Tokens[index];
                Tokens[index] = Tokens[targetIndex];
                Tokens[targetIndex] = TokenHolder;
            }

            return Tokens;
        }
        private Dictionary<int, NodeModel> ApplyTokens(Dictionary<int, char?> seedBoard, List<char> shuffledTokens)
        {
            var transformedBoard = new Dictionary<int, NodeModel>();

            // Transform values in seed board by using current value - 1 as an index to Tokens for a shuffled value
            foreach (var node in seedBoard)
            {
                var newValue = new NodeModel()
                {
                    Value = null,
                    PossibleValues = new List<int>()
                };

                if (node.Value != null)
                {
                    // IndexOf returns a 0 based index, need to offset it by one to exclude 0 from puzzle readouts
                    newValue.Value = shuffledTokens.IndexOf((char) node.Value) + 1;
                }

                transformedBoard.Add(node.Key, newValue );
            }

            return transformedBoard;
        }

        private Dictionary<int, NodeModel> RotateBoard(Dictionary<int, NodeModel> transformingBoard, int dimension)
        {
            var rotatedBoard = new Dictionary<int, NodeModel>();

            foreach (var node in transformingBoard)
            {
                int RowId = node.Key / dimension;
                int ColId = node.Key % dimension;

                int NewRowId = ColId;
                // the -1 is to account for the 0 indexing 
                int NewColId = (dimension - 1) - RowId;

                int NewId = (NewRowId * dimension) + NewColId;

                rotatedBoard.Add(NewId, node.Value);
            }

            return SortBoard(rotatedBoard);
        }
        private Dictionary<int, NodeModel> FlipBoardHoriz(Dictionary<int, NodeModel> transformingBoard, int dimension)
        {
            var flippedBoard = new Dictionary<int, NodeModel>();

            foreach (var node in transformingBoard)
            {
                int RowId = node.Key / dimension;
                int ColId = node.Key % dimension;

                int NewColId = (dimension - 1) - ColId;

                int NewId = (RowId * dimension) + NewColId;

                flippedBoard.Add(NewId, node.Value);
            }

            return SortBoard(flippedBoard);
        }
        private Dictionary<int, NodeModel> FlipBoardVert(Dictionary<int, NodeModel> transformingBoard, int dimension)
        {
            var flippedBoard = new Dictionary<int, NodeModel>();

            foreach(var node in transformingBoard)
            {
                int RowId = node.Key / dimension;
                int ColId = node.Key % dimension;

                int NewRowId = (dimension - 1) - RowId;

                int NewId = (NewRowId * dimension) + ColId;

                flippedBoard.Add(NewId, node.Value);
            }

            return SortBoard(flippedBoard);
        }
        /// <summary>
        /// This is to enforce the meaning of the node identity
        /// </summary>
        /// <param name="unorganizedBoard"></param>
        /// <returns></returns>
        private Dictionary<int, NodeModel> SortBoard(Dictionary<int, NodeModel> unorganizedBoard)
        {
            var orderedBoard = new Dictionary<int, NodeModel>();
            foreach (var node in unorganizedBoard.OrderBy(Key => Key.Key))
            {
                orderedBoard.Add(node.Key, node.Value);
            }

            return orderedBoard;
        }
        private async Task<SeedModel> LoadSeed(string difficulty, int dimension, CancellationToken cancellationToken)
        {
            var Rand = new Random();
            var index = Rand.Next(1, 3);

            var SeedDb = new SeedsDbAccess(Config);
            var Seed = await SeedDb.ReadSeed(difficulty, dimension, cancellationToken);
            return Seed;
        }

        public void Dispose()
        {
            Config = null;
        }
    }
}
