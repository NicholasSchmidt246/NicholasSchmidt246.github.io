using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Sudoku_WebService.Strategies;

namespace Sudoku_WebService.Models
{
    public class GameModel
    {
        public Guid GameId;
        public Guid PlayerId;

        public Dictionary<int, NodeModel> Board;
        public Dictionary<int, MoveModel> CompletedMoves;

        //public Dictionary<string, object> BsonBoard;
        //public Dictionary<string, object> BsonCompletedMoves;

        //public void Compress()
        //{
        //    BsonBoard = new Dictionary<string, object>();
        //    //BsonPossibleMoves = new Dictionary<string, object>();
        //    BsonCompletedMoves = new Dictionary<string, object>();

        //    foreach (var Node in Board)
        //    {
        //        BsonBoard.Add(Node.Key.ToString(), Node.Value);
        //        //BsonPossibleMoves.Add(Node.Key.ToString(), Node.Value.Item2);
        //    }

        //    foreach (var Move in CompletedMoves)
        //    {
        //        BsonCompletedMoves.Add(Move.Key.ToString(), Move.Value);
        //    }
        //}
        //public void Expand()
        //{
        //    Board = new Dictionary<int, NodeModel>();
        //    CompletedMoves = new Dictionary<int, MoveModel>();

        //    foreach (var Node in BsonBoard)
        //    {
        //        Board.Add(Convert.ToInt32(Node.Key), (NodeModel) Node.Value);
        //    }

        //    foreach (var Move in BsonCompletedMoves)
        //    {
        //        CompletedMoves.Add(Convert.ToInt32(Move.Key), (MoveModel) Move.Value);
        //    }
        //}

        ///// <summary>
        ///// Creates a SudokuPuzzle with the given dimensions
        ///// </summary>
        ///// <param name="dimension"></param>
        ///// <returns>A SudokuPuzzle object ready for play</returns>
        //public static GameModel CreatePuzzle(string difficulty = "Easy", int dimension = 9)
        //{
        //    var PuzzleGenerator = new SeedTransformationStrategy();
        //    GameModel Puzzle = new GameModel()
        //    {
        //        Board = PuzzleGenerator.GeneratePuzzleFromSeed(dimension, difficulty),
        //        CompletedMoves = new Dictionary<int, MoveModel>(),
        //        Id = Guid.NewGuid()
        //    };

        //    return Puzzle;
        //}
        ///// <summary>
        ///// Allow building of Puzzles via JSON for testing now, and for loading a saved game later
        ///// </summary>
        ///// <param name="puzzle"></param>
        ///// <returns></returns>
        //public static GameModel Build(JObject puzzle)
        //{
        //    var Puzzle = new GameModel()
        //    {
        //        id = puzzle["id"].ToObject<Guid>(),
        //        board = puzzle["board"].ToObject<Dictionary<int, int?>>(),
        //        completedMoves = puzzle["moveHistory"].ToObject<List<MoveModel>>(),
        //        dimension = puzzle["dimension"].ToObject<int>()
        //    };

        //    return Puzzle;
        //}

        ///// <summary>
        ///// Submit a Move to the SudokuPuzzle
        ///// </summary>
        ///// <param name="submittedMove"></param>
        ///// <returns>Boolean value to indicate if move was valid and submitted</returns>
        //public bool SubmitMove(MoveModel submittedMove)
        //{
        //    if (MoveValid(submittedMove))
        //    {
        //        board[submittedMove.Node] = submittedMove.Entry;

        //        if (submittedMove.Entry != null)
        //        {
        //            completedMoves.Add(submittedMove);
        //        }

        //        return true;
        //    }
        //    else return false;
        //}

        ///// <summary>
        ///// Undo the last move submitted for this puzzle
        ///// </summary>
        //public void UndoLastMove()
        //{
        //    if (completedMoves.Count > 0)
        //    {
        //        MoveModel LastMove = completedMoves.Last();
        //        completedMoves.RemoveAt(completedMoves.Count - 1);

        //        board[LastMove.Node] = null;
        //    }
        //}

        ///// <summary>
        ///// Checks a particular move against the possible values for that node
        ///// </summary>
        ///// <returns></returns>
        //private bool MoveValid(MoveModel submittedMove)
        //{
        //    if (submittedMove.Entry == null)
        //    {
        //        for (int index = completedMoves.Count - 1; index > 0; index--)
        //        {
        //            if (submittedMove.Node == completedMoves[index].Node)
        //            {
        //                completedMoves.RemoveAt(index);
        //                return true;
        //            }
        //        }
        //        return false;
        //    }

        //    List<int> possibleMoves = moves.GetMoves(board, submittedMove.Node);

        //    foreach (var move in possibleMoves)
        //    {
        //        if (move == submittedMove.Entry)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
    }
}
