using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_WebService.Models
{
    public class PossibleMovesModel
    {
        
        /// <summary>
        /// Constructor to build possible moves based on board
        /// </summary>
        /// <param name="board"></param>
        public PossibleMovesModel()
        {
        }
        /// <summary>
        /// Consolidated list of possible moves for a given location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>List<int></returns>
        public List<int> GetMoves(Dictionary<int, int?> board, int location)
        {
            var possibleInRow = PossibleMovesByRow(board, location);
            var possibleInCol = PossibleMovesByColumn(board, location);
            var possibleInCell = PossibleMovesByCell(board, location);

            var possibleMoves = new List<int>();

            foreach(var number in possibleInCell)
            {
                if(possibleInRow.Contains(number) && possibleInCol.Contains(number))
                {
                    possibleMoves.Add(number);
                }
            }

            return possibleMoves;
        }

        private List<int> PossibleMovesByRow(Dictionary<int, int?> board, int location)
        {
            int dimension = Convert.ToInt32(Math.Sqrt(board.Count));

            var possibleMoves = new List<int>();
            for (int index = 1; index <= dimension; index++)
            {
                possibleMoves.Add(index);
            }

            int targetRow = location / dimension;

            foreach (var node in board)
            {
                int RowId = node.Key / dimension;

                if(RowId != targetRow)
                {
                    break;
                }

                if(node.Value != null && possibleMoves.Contains((int)node.Value))
                {
                    possibleMoves.Remove((int)node.Value);
                }
            }

            return possibleMoves;
        }
        private List<int> PossibleMovesByColumn(Dictionary<int, int?> board, int location)
        {
            int dimension = Convert.ToInt32(Math.Sqrt(board.Count));

            var possibleMoves = new List<int>();
            for (int index = 1; index <= dimension; index++)
            {
                possibleMoves.Add(index);
            }

            int targetCol = location % dimension;

            foreach (var node in board)
            {
                int ColId = node.Key % dimension;

                if (ColId == targetCol && node.Value != null && possibleMoves.Contains((int)node.Value))
                {
                    possibleMoves.Remove((int)node.Value);
                }
            }

            return possibleMoves;
        }
        private List<int> PossibleMovesByCell(Dictionary<int, int?> board, int location)
        {
            int dimension = Convert.ToInt32(Math.Sqrt(board.Count));

            var possibleMoves = new List<int>();
            for (int index = 1; index <= dimension; index++)
            {
                possibleMoves.Add(index);
            }

            int squareRoot = Convert.ToInt32(Math.Sqrt(dimension));

            int targetRow = location / dimension;
            int targetCol = location % dimension;

            int targetTopLeftRowId = (targetRow / squareRoot) * squareRoot;
            int targetTopLeftColId = (targetCol / squareRoot) * squareRoot;

            int targetTopLeftId = (targetTopLeftRowId * dimension) + targetTopLeftColId;

            foreach (var node in board)
            {
                int nodeRow = node.Key / dimension;
                int nodeCol = node.Key % dimension;

                int topLeftRowId = (nodeRow / squareRoot) * squareRoot;
                int topLeftColId = (nodeCol / squareRoot) * squareRoot;

                int topLeftId = (topLeftRowId * dimension) + topLeftColId;

                if(topLeftId == targetTopLeftId && node.Value != null && possibleMoves.Contains((int)node.Value))
                {
                    possibleMoves.Remove((int)node.Value);
                }
            }

            return possibleMoves;
        }
    }
}
