using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Bson;
using MongoDB.Driver;

using Sudoku_WebService.Models;

namespace Sudoku_WebService.DataAccess
{
    public class PlayersDbAccess : MongoDbAccess
    {
        private const string ConnectionStringId = "MongoDb";
        private const string DatabaseName = "SudokuSolver";
        private const string CollectionName = "Players";

        public PlayersDbAccess(IConfiguration configuration) : base(configuration, ConnectionStringId, DatabaseName, CollectionName)
        {

        }

        public async Task<Guid> GetPlayerId(string email, CancellationToken cancellationToken)
        {
            if (!await PlayerExists(email, cancellationToken))
            {
                throw new Exception();
            }

            var Filter = new BsonDocument
            {
                { "Email", email }
            };

            var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
            var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

            if (ReadResults.Count != 1)
            {
                throw new Exception();
            }

            return ReadResults[0].GetValue("PlayerId").AsGuid;
        }

        public async Task<bool> PlayerExists(string email, CancellationToken cancellationToken)
        {
            var Filter = new BsonDocument
            {
                { "Email", email }
            };

            var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
            var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

            return (ReadResults.Count > 0);
        }
        public async Task<bool> PlayerExists(Guid id, CancellationToken cancellationToken)
        {
            var Filter = new BsonDocument
            {
                { "PlayerId", id.ToString() }
            };

            var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
            var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

            return (ReadResults.Count > 0);
        }

        #region CRUD Methods

        public async Task<bool> CreatePlayer(PlayerModel userData, CancellationToken cancellationToken)
        {
            Guid Id = Guid.NewGuid();

            // Verify initialized Guid does not already exist, if it does, generate a new Id
            while(await PlayerExists(Id, cancellationToken))
            {
                Id = Guid.NewGuid();
            }

            // Verify Email is not already used for another Id
            if(await PlayerExists(userData.Email, cancellationToken))
            {
                throw new ArgumentException("Account already exists", "Email");
            }

            // Adding a record here, will not create a duplicate.
            var document = new BsonDocument()
            {
                { "PlayerId", Id.ToString() },
                { "Email", userData.Email },
                { "UserName", userData.UserName }
            };

            await CreateAsync(document, cancellationToken);

            return await PlayerExists(Id, cancellationToken); // Test to confirm document was added
        }
        public async Task<PlayerModel> ReadPlayer(Guid id, CancellationToken cancellationToken)
        {
            if (!await PlayerExists(id, cancellationToken))
            {
                throw new ArgumentException("Unauthorized");
            }

            var Filter = Builders<BsonDocument>.Filter.Eq("PlayerId", id.ToString());

            var ReadCursorResults = await ReadAsync(Filter, cancellationToken);
            var ReadResults = await ReadCursorResults.ToListAsync(cancellationToken);

            if(ReadResults.Count != 1)
            {
                throw new Exception();
            }

            ReadResults[0].Remove("_id");

            var Player = new PlayerModel()
            {
                UserId = ReadResults[0]["PlayerId"].AsGuid,
                Email = ReadResults[0]["Email"].AsString,
                UserName = ReadResults[0]["UserName"].AsString
            };

            return Player;
        }
        public async Task<bool> UpdatePlayer(PlayerModel userData, CancellationToken cancellationToken)
        {
            if (!await PlayerExists(userData.UserId, cancellationToken))
            {
                throw new ArgumentException("");
            }

            var Builder = Builders<BsonDocument>.Filter;
            var Filter = Builder.Eq("PlayerId", userData.UserId.ToString());
            var Update = Builders<BsonDocument>.Update.Set("Email", userData.Email).Set("UserName", userData.UserName);

            var UpdateResult = await UpdateAsync(Filter, Update, cancellationToken);
            return (UpdateResult.IsAcknowledged && UpdateResult.MatchedCount > 0 && UpdateResult.ModifiedCount > 0);
        }
        public async Task<bool> DeletePlayer(Guid id, CancellationToken cancellationToken)
        {
            if (!await PlayerExists(id, cancellationToken))
            {
                throw new ArgumentException("");
            }

            var Filter = Builders<BsonDocument>.Filter.Eq("PlayerId", id.ToString());

            var DeleteResult = await DeleteAsync(Filter, cancellationToken);
            return (DeleteResult.IsAcknowledged && DeleteResult.DeletedCount > 0);
        }

        #endregion
    }
}
