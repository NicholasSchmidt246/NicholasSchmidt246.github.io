//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//using Sudoku_WebService.Models;

//using Sudoku_WebService;

//namespace Sudoku_WebService.Strategies
//{
//    public class PlayStrategy : IDisposable
//    {
//        private Guid UserId = Guid.Empty;
//        private Guid PuzzleId = Guid.Empty;

//        protected GameModel GameBoard;

//        #region Constructors

//        public PlayStrategy(Guid userId)
//        {
//            UserId = userId;
//        }
//        public PlayStrategy(Guid userId, Guid puzzleId)
//        {
//            UserId = userId;
//            PuzzleId = puzzleId;
//        }

//        #endregion

//        #region Puzzle Manipulators

//        private async Task<Guid> BuildNewPuzzle(string difficulty, int dimension)
//        {
//            // Validate input
//            if (!UserInputValidationStrategy.ValidateDifficulty(difficulty))
//            {
//                throw new ArgumentException($"Invalid value: {difficulty}", "difficulty");
//            }
//            if (!UserInputValidationStrategy.ValidateDimension(dimension))
//            {
//                throw new ArgumentException($"Invalid value: {dimension}", "dimension");
//            }

//            await Task.Delay(0); // TODO: The Seeds should sit in a db.
//            GameBoard = GameModel.CreatePuzzle(difficulty, dimension);

//            return await SavePuzzleToDbGetId();
//        }
//        private async Task<Guid> SavePuzzleToDbGetId()
//        {
//            // TODO: Save Puzzle to Db
//            // TODO: Update puzzleId
//            PuzzleId = Guid.Empty;
//            await Task.Delay(0);
//            return PuzzleId;
//        }
//        private async Task<bool> DeletePuzzleWithId()
//        {
//            if (PuzzleId == Guid.Empty)
//            {
//                throw new ArgumentException($"Invalid value: {PuzzleId}", "puzzleId");
//            }

//            await Task.Delay(0);
//            // TODO: Delete Puzzle From Db
//            return false;
//        }
//        private async Task LoadPuzzleWithId()
//        {
//            if (PuzzleId == Guid.Empty)
//            {
//                throw new ArgumentException($"Invalid value: {PuzzleId}", "puzzleId");
//            }

//            // TODO: Get Puzzle From DB
//            // TODO: Load Puzzle into GameBoard

//            await Task.Delay(0);
//        }

//        #endregion
//        #region Move Manipulators

//        private int GetMoveCountFromGameBoard()
//        {
//            if (PuzzleId == Guid.Empty)
//            {
//                throw new ArgumentException($"Invalid value: {PuzzleId}", "puzzleId");
//            }

//            return GameBoard.completedMoves.Count;
//        }
//        private async Task SaveState(bool saveMoves)
//        {
//            if(saveMoves)
//            {
//                // TODO: Save Moves
//                await Task.Delay(0);
//            }

//            // TODO: Save Board
//            await Task.Delay(0);
//        }
//        private async Task<bool> MakeMove(MoveModel move)
//        {
//            if (PuzzleId == Guid.Empty)
//            {
//                throw new ArgumentException($"Invalid value: {PuzzleId}", "puzzleId");
//            }

//            if(GameBoard.SubmitMove(move))
//            {
//                await SaveState(true);
//                return true;
//            }

//            return false;
//        }
//        private async Task<bool> UndoMove()
//        {
//            if (PuzzleId == Guid.Empty)
//            {
//                throw new ArgumentException($"Invalid value: {PuzzleId}", "puzzleId");
//            }

//            GameBoard.UndoLastMove();

//            await SaveState(true);

//            return true;
//        }

//        #endregion

//        #region PuzzlesController Interface

//        // These methods are just decorators of our internal methods and should not be doing any work outside of that.

//        public async Task<Stream> DeletePuzzle(string contentType)
//        {
//            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

//            bool DeleteSuccessful = await DeletePuzzleWithId();

//            var DeletePuzzleResponse = new Dictionary<string, bool>
//            {
//                { "DeleteSuccessful", DeleteSuccessful }
//            };

//            return ContentTypeTransformer.FormatContent(ContentType, DeletePuzzleResponse);
//        }
//        public async Task<Stream> GetPuzzle(string contentType)
//        {
//            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

//            await LoadPuzzleWithId();

//            return ContentTypeTransformer.FormatContent(ContentType, GameBoard);
//        }
//        public async Task<Stream> GetPuzzle(string contentType, string difficulty, int dimension)
//        {
//            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

//            await BuildNewPuzzle(difficulty, dimension);

//            return ContentTypeTransformer.FormatContent(ContentType, GameBoard);
//        }

//        #endregion
//        #region MovesController Interface

//        // These methods are just decorators of our internal methods and should not be doing any work outside of that.

//        public async Task<Stream> GetMoveCount(string contentType)
//        {
//            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

//            await LoadPuzzleWithId();
//            var moveCount = GetMoveCountFromGameBoard();

//            var MoveCountResponse = new Dictionary<string, int>
//            {
//                { "Moves Made", moveCount }
//            };

//            return ContentTypeTransformer.FormatContent(ContentType, MoveCountResponse);

//        }
//        public async Task<Stream> MakeMove(string contentType, string move)
//        {
//            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

//            if (!UserInputValidationStrategy.ValidateMove(move))
//            {
//                throw new ArgumentException($"Invalid value: {move}", "move");
//            }
//            // TODO: Serialize move into MoveModel

//            await LoadPuzzleWithId();
//            var IsValidMove = await MakeMove(new MoveModel());
//            var MakeMoveResponse = new Dictionary<string, bool>
//            {
//                { "ValidMove", IsValidMove }
//            };

//            return ContentTypeTransformer.FormatContent(ContentType, MakeMoveResponse);
//        }
//        public async Task<Stream> DeleteMove(string contentType)
//        {
//            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

//            await LoadPuzzleWithId();
//            bool DeleteSuccessful = await UndoMove();

//            var DeleteMoveResponse = new Dictionary<string, bool>
//            {
//                { "DeleteSuccessful", DeleteSuccessful }
//            };

//            return ContentTypeTransformer.FormatContent(ContentType, DeleteMoveResponse);
//        }

//        #endregion

//        public void Dispose()
//        {
//            UserId = Guid.Empty;
//            PuzzleId = Guid.Empty;
//            GameBoard = null;
//        }
//    }
//}
