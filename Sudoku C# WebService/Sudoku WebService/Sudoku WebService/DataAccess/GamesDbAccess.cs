using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;

using Sudoku_WebService.Models;

namespace Sudoku_WebService.DataAccess
{
    public class GamesDbAccess : MongoDbAccess
    {
        private const string ConnectionStringId = "MongoDb";
        private const string DatabaseName = "SudokuSolver";
        private const string CollectionName = "Games";

        public GamesDbAccess(IConfiguration configuration) :  base(configuration, ConnectionStringId, DatabaseName, CollectionName)
        {

        }

        public async Task<bool> GameExists(Guid gameId, CancellationToken cancellationToken)
        {
            var Filter = new BsonDocument
            {
                { "GameId", gameId.ToString() }
            };

            var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
            var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

            return (ReadResults.Count > 0);
        }

        public async Task<Guid> CreateGame(GameModel game, CancellationToken cancellationToken)
        {
            Guid gameId = Guid.NewGuid();

            // Verify initialized Guid does not already exist, if it does, generate a newgameId
            while (await GameExists(gameId, cancellationToken))
            {
               gameId = Guid.NewGuid();
            }

            game.GameId =gameId;
            var jsonDoc = JsonConvert.SerializeObject(game);
            var bsonDoc = BsonSerializer.Deserialize<BsonDocument>(jsonDoc);
            
            await CreateAsync(bsonDoc, cancellationToken);

            return gameId;
        }
        public async Task<GameModel> ReadGame(Guid gameId, Guid playerId, CancellationToken cancellationToken)
        {
            if (!await GameExists(gameId, cancellationToken))
            {
                throw new ArgumentException("Unauthorized");
            }

            var Filter = new BsonDocument()
            {
                { "GameId",gameId.ToString() },
                { "PlayerId", playerId.ToString() }
            };

            var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
            var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

            if (ReadResults.Count != 1)
            {
                throw new Exception();
            }

            ReadResults[0].Remove("_id");

            GameModel Game = BsonSerializer.Deserialize<GameModel>(ReadResults[0].AsBsonDocument);

            return Game;
        }
        public async Task<bool> UpdateGame(Guid playerId, Guid gameId, GameModel game, CancellationToken cancellationToken)
        {
            if (!await GameExists(gameId, cancellationToken))
            {
                throw new ArgumentException("");
            }

            var Filter = new BsonDocument()
            {
                { "GameId", gameId.ToString() },
                { "PlayerId", playerId.ToString() }
            };

            var completedMoves = new Dictionary<string, MoveModel>();
            foreach(var completedMove in game.CompletedMoves)
            {
                completedMoves.Add(completedMove.Key.ToString(), completedMove.Value);
            }
            var board = new Dictionary<string, NodeModel>();
            foreach(var node in game.Board)
            {
                board.Add(node.Key.ToString(), node.Value);
            }

            var Update = Builders<BsonDocument>.Update.Set("CompletedMoves", completedMoves).Set("Board", board);

            var UpdateResult = await UpdateAsync(Filter, Update, cancellationToken);
            return (UpdateResult.IsAcknowledged && UpdateResult.MatchedCount > 0 && UpdateResult.ModifiedCount > 0);
        }
        public async Task<bool> DeleteGame(Guid playerId, Guid gameId, CancellationToken cancellationToken)
        {
            if (!await GameExists(gameId, cancellationToken))
            {
                throw new ArgumentException("");
            }

            var Filter = new BsonDocument()
            {
                { "GameId", gameId.ToString() },
                { "PlayerId", playerId.ToString() }
            };

            var DeleteResult = await DeleteAsync(Filter, cancellationToken);
            return (DeleteResult.IsAcknowledged && DeleteResult.DeletedCount > 0);
        }
    }
}
