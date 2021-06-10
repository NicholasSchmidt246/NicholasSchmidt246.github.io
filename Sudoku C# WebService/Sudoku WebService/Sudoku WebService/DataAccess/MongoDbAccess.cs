using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace Sudoku_WebService.DataAccess
{
    public class MongoDbAccess : IDisposable
    {
        private MongoClient Client;
        private IMongoDatabase Database;
        private IMongoCollection<BsonDocument> Collection;

        public MongoDbAccess(IConfiguration configuration, string connectionStringId, string dbName, string collectionName)
        {
            var ConnectionString = ServiceConfigurations.GetConnectionString(connectionStringId, configuration);

            var MongoSettings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            MongoSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var DatabaseSettings = new MongoDatabaseSettings();

            var CollectionSettings = new MongoCollectionSettings();

            Client = new MongoClient(MongoSettings);
            Database = Client.GetDatabase(dbName, DatabaseSettings);
            Collection = Database.GetCollection<BsonDocument>(collectionName, CollectionSettings);
        }

        #region CRUD Functions

        protected async Task CreateAsync(BsonDocument document, CancellationToken cancellationToken)
        {
            var Options = new InsertOneOptions();
            await Collection.InsertOneAsync(document, Options, cancellationToken);
        }
        protected async Task<DeleteResult> DeleteAsync(FilterDefinition<BsonDocument> filter, CancellationToken cancellationToken)
        {
            var Options = new DeleteOptions();
            return await Collection.DeleteManyAsync(filter, Options, cancellationToken);
        }
        protected async Task<IAsyncCursor<BsonDocument>> ReadAsync(FilterDefinition<BsonDocument> filter, CancellationToken cancellationToken)
        {
            var Options = new FindOptions<BsonDocument>();
            return await Collection.FindAsync(filter, Options, cancellationToken);
        }
        protected async Task<UpdateResult> UpdateAsync(FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update, CancellationToken cancellationToken)
        {
            var Options = new UpdateOptions();
            return await Collection.UpdateOneAsync(filter, update, Options, cancellationToken);
        }

        #endregion

        public void Dispose()
        {
            Client = null;
            Database = null;
            Collection = null;
        }
    }
}
