using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Sudoku_WebService.Formatters;
using Sudoku_WebService.Models;

namespace Sudoku_WebService.Strategies
{
    public class SolverStrategy
    {
        private GameModel Game;

        public SolverStrategy()
        {

        }

        private Dictionary<int, NodeModel> BruteForce(ContentTypeTransformer.UnifiedContentType contentType, string game)
        {
            var Game = ContentTypeTransformer.CreateObjectFromContent<GameModel>(contentType, game);

            if (Solve(0))
            {
                return Game.Board;
            }
            return null;
        }
        /// <summary>
        /// I apologize for the recursion, I did not have time to impliment some of the fancier solution strategies and instead used only the migrated tool
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool Solve(int node)
        {
            var possibleValues = Game.Board[node].PossibleValues;

            if (node < Game.Board.Count)
            {
                if (Game.Board[node].Value == null)
                {
                    if (possibleValues == null || possibleValues.Count <= 0)
                    {
                        return false; // Exception?
                    }
                    else
                    {
                        Game.Board[node].Value = possibleValues[0];

                        while (!Solve(node + 1))
                        {
                            possibleValues.RemoveAt(0);

                            if (possibleValues == null || possibleValues.Count <= 0)
                            {
                                return false;
                            }
                            Game.Board[node].Value = possibleValues[0];
                        }

                        return true;
                    }
                }
                else
                {
                    return Solve(node + 1);
                }
            }

            return true;
        }

        #region SolutionsController Interface

        public Stream GetSolution(string inputContentType, string outputContentType, string puzzle)
        {
            var inContentType = ContentTypeTransformer.UnifyContentType(inputContentType);
            var outContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            GameModel Game = new GameModel
            {
                Board = BruteForce(inContentType, puzzle)
            };

            return ContentTypeTransformer.FormatContent(outContentType, Game.Board);
        }

        #endregion
    }
}
