using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Sudoku_WebService.Formatters;
using Sudoku_WebService.DataAccess;
using Sudoku_WebService.Models;

namespace Sudoku_WebService.Strategies
{
    public class PlayStrategy : IDisposable
    {
        private IConfiguration Config;

        #region Constructors

        public PlayStrategy(IConfiguration config)
        {
            Config = config;
        }

        #endregion

        #region Puzzle Manipulators

        private async Task<GameModel> BuildNewPuzzle(Guid userId, string difficulty, int dimension, CancellationToken cancellationToken)
        {
            // Validate input
            if (!UserInputValidationStrategy.ValidateDifficulty(difficulty))
            {
                throw new ArgumentException($"Invalid value: {difficulty}", "difficulty");
            }
            if (!UserInputValidationStrategy.ValidateDimension(dimension))
            {
                throw new ArgumentException($"Invalid value: {dimension}", "dimension");
            }

            GameModel NewGame;

            using (var SeedTrans = new SeedTransformationStrategy(Config))
            {
                NewGame = await SeedTrans.GeneratePuzzleFromSeed(userId, difficulty, dimension, cancellationToken);
            }

            var GamesDb = new GamesDbAccess(Config);
            var GameId = await GamesDb.CreateGame(NewGame, cancellationToken);

            var Game = await LoadPuzzleWithId(userId, GameId, cancellationToken);

            return Game;
        }
        private async Task<bool> DeletePuzzleWithId(Guid userId, Guid gameId, CancellationToken cancellationToken)
        {
            if (gameId == Guid.Empty)
            {
                throw new ArgumentException($"Invalid value: {gameId}", "gameId");
            }

            var GamesDb = new GamesDbAccess(Config);
            var GameId = await GamesDb.DeleteGame(userId, gameId, cancellationToken);

            return false;
        }
        private async Task<GameModel> LoadPuzzleWithId(Guid userId, Guid gameId, CancellationToken cancellationToken)
        {
            if (gameId == Guid.Empty)
            {
                throw new ArgumentException($"Invalid value: {gameId}", "gameId");
            }

            var GamesDb = new GamesDbAccess(Config);
            var Game = await GamesDb.ReadGame(gameId, userId, cancellationToken);

            return Game;
        }

        #endregion
        #region Move Manipulators

        private async Task<int> GetMoveCountFromGameBoard(Guid userId, Guid gameId, CancellationToken cancellationToken)
        {
            if (gameId == Guid.Empty)
            {
                throw new ArgumentException($"Invalid value: {gameId}", "gameId");
            }

            var Game = await LoadPuzzleWithId(userId, gameId, cancellationToken);

            return Game.CompletedMoves.Count;
        }
        private async Task<bool> MakeMove(ContentTypeTransformer.UnifiedContentType contentType, Guid userId, Guid gameId, string move, CancellationToken cancellationToken)
        {
            if (userId != Guid.Empty && gameId == Guid.Empty)
            {
                if (gameId == Guid.Empty)
                    throw new ArgumentException($"Invalid value: {gameId}", "Game Id");
                else
                    throw new ArgumentException($"Invalid value: {userId}", "User Id");
            }
            if (!UserInputValidationStrategy.ValidateMove(move))
            {
                throw new ArgumentException();
            }

            var Move = ContentTypeTransformer.CreateObjectFromContent<MoveModel>(contentType, move);
            var Game = await LoadPuzzleWithId(userId, gameId, cancellationToken);

            // Ensure node is NOT a starting value
            if(Game.Board[Move.Node].Value != null)
            {
                bool IsValidMove = false;

                foreach(var completedMove in Game.CompletedMoves)
                {
                    if(completedMove.Value.Node == Move.Node)
                    {
                        // Not a starting value
                        IsValidMove = true;
                        break;
                    }
                }

                if(!IsValidMove)
                {
                    throw new ArgumentOutOfRangeException("Cannot change a starting value");
                }
            }

            if(Move.Entry != null)
            {
                if(!Game.Board[Move.Node].PossibleValues.Contains((int)Move.Entry))
                {
                    throw new ArgumentOutOfRangeException("Submitted entry is not possible in requested node");
                }
            }

            // Make move
            Game.Board[Move.Node].Value = Move.Entry;
            Game.CompletedMoves.Add(Game.CompletedMoves.Count + 1, Move);

            // Update possible moves for this and related nodes
            Game.PopulatePossibleMoves(Move.Node);
            var RelatedNodes = Game.GetRelatedNodes(Move.Node);

            foreach (var Node in RelatedNodes)
            {
                Game.PopulatePossibleMoves(Node);
            }

            // Save to Db
            var GamesDb = new GamesDbAccess(Config);
            bool Success = await GamesDb.UpdateGame(userId, gameId, Game, cancellationToken);

            return Success;
        }
        private async Task<bool> UndoMove(Guid userId, Guid gameId, CancellationToken cancellationToken)
        {
            var Game = await LoadPuzzleWithId(userId, gameId, cancellationToken);

            var Move = Game.CompletedMoves.Last().Value;

            // Undo Move
            Game.Board[Move.Node].Value = null;
            Game.CompletedMoves.Remove(Game.CompletedMoves.Count - 1);

            // Update possible moves for this and related nodes
            Game.PopulatePossibleMoves(Move.Node);
            var RelatedNodes = Game.GetRelatedNodes(Move.Node);

            foreach (var Node in RelatedNodes)
            {
                Game.PopulatePossibleMoves(Node);
            }

            var GamesDb = new GamesDbAccess(Config);
            bool Success = await GamesDb.UpdateGame(userId, gameId, Game, cancellationToken);

            return Success;
        }

        #endregion

        #region PuzzlesController Interface

        // These methods are just decorators of our internal methods and should not be doing any work outside of that.

        public async Task<Stream> DeletePuzzle(string outputContentType, Guid userId, Guid gameId, CancellationToken cancellationToken)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            bool DeleteSuccessful = await DeletePuzzleWithId(userId, gameId, cancellationToken);

            var DeletePuzzleResponse = new Dictionary<string, bool>
            {
                { "DeleteSuccessful", DeleteSuccessful }
            };

            return ContentTypeTransformer.FormatContent(ContentType, DeletePuzzleResponse);
        }
        public async Task<Stream> GetPuzzle(string outputContentType, Guid userId, Guid gameId, CancellationToken cancellationToken)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            var Game = await LoadPuzzleWithId(userId, gameId, cancellationToken);

            return ContentTypeTransformer.FormatContent(ContentType, Game.Board);
        }
        public async Task<Stream> GetPuzzle(string outputContentType, Guid userId, string difficulty, int dimension, CancellationToken cancellationToken)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            var Game = await BuildNewPuzzle(userId, difficulty, dimension, cancellationToken);

            return ContentTypeTransformer.FormatContent(ContentType, Game.Board);
        }

        #endregion
        #region MovesController Interface

        // These methods are just decorators of our internal methods and should not be doing any work outside of that.

        public async Task<Stream> GetMoveCount(string contentType, Guid userId, Guid gameId, CancellationToken cancellationToken)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

            var moveCount = await GetMoveCountFromGameBoard(userId, gameId, cancellationToken);

            var MoveCountResponse = new Dictionary<string, int>
            {
                { "Moves Made", moveCount }
            };

            return ContentTypeTransformer.FormatContent(ContentType, MoveCountResponse);

        }
        public async Task<Stream> MakeMove(string inputContentType, string outputContentType, Guid userId, Guid gameId, string move, CancellationToken cancellationToken)
        {
            var inContentType = ContentTypeTransformer.UnifyContentType(inputContentType);
            var outContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            var IsValidMove = await MakeMove(inContentType, userId, gameId, move, cancellationToken);
            var MakeMoveResponse = new Dictionary<string, bool>
            {
                { "ValidMove", IsValidMove }
            };

            return ContentTypeTransformer.FormatContent(outContentType, MakeMoveResponse);
        }
        public async Task<Stream> DeleteMove(string contentType, Guid userId, Guid gameId, CancellationToken cancellationToken)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

            bool DeleteSuccessful = await UndoMove(userId, gameId, cancellationToken);

            var DeleteMoveResponse = new Dictionary<string, bool>
            {
                { "DeleteSuccessful", DeleteSuccessful }
            };

            return ContentTypeTransformer.FormatContent(ContentType, DeleteMoveResponse);
        }

        #endregion

        public void Dispose()
        {
            Config = null;
        }
    }
}
