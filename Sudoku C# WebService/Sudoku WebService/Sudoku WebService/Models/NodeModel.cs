using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_WebService.Models
{
    public class NodeModel
    {
        public int? Value;
        public List<int> PossibleValues;
    }
}
