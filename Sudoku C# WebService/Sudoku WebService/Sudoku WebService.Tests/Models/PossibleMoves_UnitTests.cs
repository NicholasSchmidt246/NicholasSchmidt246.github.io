using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

using Sudoku_WebService.Tests.TestInput;

using Sudoku_WebService.Models;
using Sudoku_WebService.Strategies;

namespace Sudoku_WebService.Tests.Models
{
    public class PossibleMoves_UnitTests
    {
        //[Theory]
        //[InlineData("SamplePuzzle1")]
        //public void SeedTransformer_GeneratePuzzle(string SamplePuzzle)
        //{
        //    JObject PuzzleToLoad = JsonToJObject.GetSample(SamplePuzzle);
        //    var board = PuzzleToLoad["board"].ToObject<Dictionary<int, int?>>();
        //    var ST = new SeedTransformer(9);
        //    var Puzzle = ST.GeneratePuzzleFromSeed("Easy");

        //    var Moves = new PossibleMovesModel(Puzzle);
        //    Moves.PossibleMovesByCell(Puzzle, 16);
        //}
    }
}
