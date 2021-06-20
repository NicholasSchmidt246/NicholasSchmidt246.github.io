using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

using Sudoku_WebService.Tests.TestInput;

using Sudoku_WebService.Models;
using Sudoku_WebService.Strategies;

namespace Sudoku_WebService.Tests.Strategies
{
    public class SeedTransformer_UnitTests
    {
        //[Theory]
        //[InlineData("SamplePuzzle1")]
        //public void SeedTransformer_FlipHoriz_9x9(string SamplePuzzle)
        //{
        //    JObject PuzzleToLoad = JsonToJObject.GetSample(SamplePuzzle);
        //    var board = PuzzleToLoad["board"].ToObject<Dictionary<int, int?>>();
        //    var Puzzle = new SeedTransformer(9).FlipBoardHoriz(board);
        //}
        //[Theory]
        //[InlineData("SamplePuzzle1")]
        //public void SeedTransformer_FlipVert(string SamplePuzzle)
        //{
        //    JObject PuzzleToLoad = JsonToJObject.GetSample(SamplePuzzle);
        //    var board = PuzzleToLoad["board"].ToObject<Dictionary<int, int?>>();
        //    var Puzzle = new SeedTransformer(9).FlipBoardVert(board);
        //}
        //[Theory]
        //[InlineData("SamplePuzzle1")]
        //public void SeedTransformer_Rotate(string SamplePuzzle)
        //{
        //    JObject PuzzleToLoad = JsonToJObject.GetSample(SamplePuzzle);
        //    var board = PuzzleToLoad["board"].ToObject<Dictionary<int, int?>>();
        //    var Puzzle = new SeedTransformer(9).RotateBoard(board);
        //}
        //[Theory]
        //[InlineData("SamplePuzzle1")]
        //public void SeedTransformer_ApplyTokens(string SamplePuzzle)
        //{
        //    JObject PuzzleToLoad = JsonToJObject.GetSample(SamplePuzzle);
        //    var board = PuzzleToLoad["board"].ToObject<Dictionary<int, int?>>();
        //    var ST = new SeedTransformer(9);
        //    List<int> Tokens = ST.ShuffleTokens();
        //    var Puzzle = ST.ApplyTokens(board);
        //}
        //[Theory]
        //[InlineData("SamplePuzzle1")]
        //public void SeedTransformer_GeneratePuzzle(string SamplePuzzle)
        //{
        //    JObject PuzzleToLoad = JsonToJObject.GetSample(SamplePuzzle);
        //    var board = PuzzleToLoad["board"].ToObject<Dictionary<int, int?>>();
        //    var ST = new SeedTransformer(9);
        //    var Puzzle = ST.GeneratePuzzleFromSeed("Easy");
        //}
    }
}
