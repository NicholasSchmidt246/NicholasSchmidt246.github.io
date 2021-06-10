using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_WebService.Models
{
    public class SeedModel
    {
        public Dictionary<int, string> Board;
        public int Dimension;
        public string Difficulty;
        public Guid SeedId;
    }
}
