using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Sudoku_WebService.Strategies
{
    public class SeedTransformationStrategy
    {
        private int dimension;
        private List<int> Tokens = new List<int>();
        private Dictionary<int, int?> seedBoard;

        public SeedTransformationStrategy(int dimension)
        {
            this.dimension = dimension;
            for(int index = 1; index <= dimension; index++)
            {
                Tokens.Add(index);
            }
        }

        private void InitializeTokens()
        { 
        }
        
        public Dictionary<int, int?> GeneratePuzzleFromSeed(string difficulty)
        {
            var board = new Dictionary<int, int?>();
            // Load starting seed
            LoadSeed(difficulty);

            // Determine Transformations
            var Rand = new Random();
            var rotations = Rand.Next(0, 4);
            var flipHoriz = Rand.Next(0, 2);
            var flipVert = Rand.Next(0, 2);

            // Execute Transformations
            while (rotations > 0)
            {
                board = RotateBoard(seedBoard);
                rotations = rotations - 1;
            }
            while (flipHoriz > 0)
            {
                board = FlipBoardHoriz(board);
                flipHoriz = flipHoriz - 1;
            }
            while (flipVert > 0)
            {
                board = FlipBoardVert(board);
                flipVert = flipVert - 1;
            }

            // Execute Token Shuffle
            ShuffleTokens();
            board = ApplyTokens(board);

            return board;
        }

        /// <summary>
        /// Shuffle the tokens to use to swap values in the seed
        /// </summary>
        public List<int> ShuffleTokens()
        {
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
        public Dictionary<int, int?> ApplyTokens(Dictionary<int, int?> transformedBoard)
        {
            var updatedSeedBoard = new Dictionary<int, int?>();

            // Transform values in seed board by using current value - 1 as an index to Tokens for a shuffled value
            foreach (var node in transformedBoard)
            {
                int? newValue = node.Value;

                if (node.Value != null)
                {
                    newValue = Tokens[(int)newValue - 1];
                }

                updatedSeedBoard.Add(node.Key, newValue);
            }

            return updatedSeedBoard;
        }

        public Dictionary<int, int?> RotateBoard(Dictionary<int, int?> transformingBoard)
        {
            var rotatedBoard = new Dictionary<int, int?>();

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
        public Dictionary<int, int?> FlipBoardHoriz(Dictionary<int, int?> transformingBoard)
        {
            var flippedBoard = new Dictionary<int, int?>();

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
        public Dictionary<int, int?> FlipBoardVert(Dictionary<int, int?> transformingBoard)
        {
            var flippedBoard = new Dictionary<int, int?>();

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
        private Dictionary<int, int?> SortBoard(Dictionary<int, int?> unorganizedBoard)
        {
            var orderedBoard = new Dictionary<int, int?>();
            foreach (var node in unorganizedBoard.OrderBy(Key => Key.Key))
            {
                orderedBoard.Add(node.Key, node.Value);
            }

            return orderedBoard;
        }
        private void LoadSeed(string difficulty)
        {
            var Rand = new Random();
            var index = Rand.Next(1, 3);

            var SeedJObject = JObject.Parse(File.ReadAllText($"../../../../Sudoku WebService/PuzzleSeeds/{difficulty}Seed{index}_{dimension}x{dimension}.json"));
            seedBoard = SeedJObject.ToObject<Dictionary<int, int?>>();
        }
    }
}
