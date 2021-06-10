using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using Sudoku_WebService.Models;

namespace Sudoku_WebService.DataAccess
{
    public class SeedsDbAccess : MongoDbAccess
    {
        private const string ConnectionStringId = "MongoDb";
        private const string DatabaseName = "SudokuSolver";
        private const string CollectionName = "Seeds";

        public SeedsDbAccess(IConfiguration configuration) : base(configuration, ConnectionStringId, DatabaseName, CollectionName)
        {

        }

        #region CRUD

        public async Task<SeedModel> ReadSeed(string difficulty, int dimension, CancellationToken cancellationToken)
        {
            var Filter = new BsonDocument
            {
                { "SeedDifficulty", $"{difficulty}" },
                { "SeedDimension", $"{dimension}x{dimension}" }
            };

            var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
            var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

            var Rand = new Random();
            int index = Rand.Next(0, ReadResults.Count + 1);

            SeedModel Seed = BsonSerializer.Deserialize<SeedModel>(ReadResults[index].AsBsonDocument);

            return Seed;
        }

        #endregion
    }
}
