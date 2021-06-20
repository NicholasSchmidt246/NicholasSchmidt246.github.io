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

        public Dictionary<string, object> BsonBoard;
        public Dictionary<string, object> BsonCompletedMoves;

        // Iterate through the entire board to populate possible moves, ideally this is only ever done once
        public void PopulatePossibleMoves()
        {
            foreach(var Node in Board)
            {
                PopulatePossibleMoves(Node.Key);
            }
        }
        public void PopulatePossibleMoves(int node)
        {
            var possibleMoves = GeneratePossibleMoveTemplate();
            var MovesMadeOnNode = CompletedMoves.Values.Any(move => move.Node == node);

            if (Board[node].Value == null || MovesMadeOnNode) // Ignore populating possible moves for starting values
            {
                var RelatedNodes = GetRelatedNodes(node);

                foreach (var RelatedNode in RelatedNodes)
                {
                    RemoveNodeValueFromPossibleMoves(RelatedNode, ref possibleMoves);
                }
            }
            else
            {
                possibleMoves = null;
            }

            Board[node].PossibleValues = possibleMoves;
        }
        public List<int> GetRelatedNodes(int node)
        {
            var RelatedNodeList = new List<int>();

            int dimension = Convert.ToInt32(Math.Sqrt(Board.Count));

            int Row = node / dimension;
            int Col = node % dimension;

            int squareRoot = Convert.ToInt32(Math.Sqrt(dimension));

            int TopLeftRowId = (Row / squareRoot) * squareRoot;
            int TopLeftColId = (Col / squareRoot) * squareRoot;

            int Cell = (TopLeftRowId * dimension) + TopLeftColId;

            // foreach node in Row
            for (int index = (Row * dimension); index < ((Row + 1) * dimension); index += 1)
            {
                if (index != node) // Ignore current location
                {
                    RelatedNodeList.Add(index);
                }
            }

            // foreach node in Col
            for (int index = Col; index < Board.Count; index += dimension)
            {
                if (index != node) // Ignore current location
                {
                    RelatedNodeList.Add(index);
                }
            }

            // foreach node in Cell
            for (int ColStart = Cell; ColStart < (dimension * squareRoot) + Cell; ColStart = ((ColStart / dimension) + 1) * dimension)
            {
                for (int index = ColStart; index < (ColStart + squareRoot); index += 1)
                {
                    if (index != node && (index / dimension) != Row && (index % dimension) != Col) // Ignore current location, and locations previously added by Row or Column
                    {
                        RelatedNodeList.Add(index);
                    }
                }
            }

            return RelatedNodeList;
        }
        private void RemoveNodeValueFromPossibleMoves(int targetNode, ref List<int> possibleMoves)
        {
            var node = Board[targetNode];
            if (node.Value != null && possibleMoves.Contains((int)node.Value))
            {
                possibleMoves.Remove((int)node.Value);
            }
        }
        private List<int> GeneratePossibleMoveTemplate()
        {
            var Template = new List<int>();

            int dimension = Convert.ToInt32(Math.Sqrt(Board.Count));

            for(int index = 1; index <= dimension; index++)
            {
                Template.Add(index);
            }

            return Template;
        }
    }
}
