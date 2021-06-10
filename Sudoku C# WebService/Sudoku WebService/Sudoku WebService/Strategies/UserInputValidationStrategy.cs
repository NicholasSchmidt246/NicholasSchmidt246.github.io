using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_WebService.Strategies
{
    public static class UserInputValidationStrategy
    {
        /// <summary>
        /// Currently, only easy puzzles are supported for puzzle generation
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public static bool ValidateDifficulty(string difficulty)
        {
            switch (difficulty.ToLower())
            {
                case "easy":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Currently, only 4x4, 9x9, and 16x16 puzzles are supported for puzzle generation
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public static bool ValidateDimension(int dimension)
        {
            switch (dimension)
            {
                case 4: // These are fine to cascade as they are valid cases and the action should be the same
                case 9:
                case 16:
                    return true;
                default:
                    return false;
            }
        }
        public static bool ValidateUserData(string userData)
        {
            return false;
        }
        public static bool ValidateMove(string move)
        {
            return false;
        }
    }
}
