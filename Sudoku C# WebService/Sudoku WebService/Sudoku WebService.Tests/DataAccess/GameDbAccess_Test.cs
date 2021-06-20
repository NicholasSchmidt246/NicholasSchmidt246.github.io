using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;

using Sudoku_WebService.Tests.TestInput;

using Sudoku_WebService.DataAccess;
using Sudoku_WebService.Models;
using Sudoku_WebService.Strategies;

namespace Sudoku_WebService.Tests.DataAccess
{
    public class GameDbAccess_Test
    {
        private const string PlayerId = "43214893-3865-43d0-8287-c38c8c971fc0";

        [Fact]
        //[InlineData()]
        public async Task Create()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://nickstestmongodb:kbpIJs6Kmj0viH8exEbRucTwWuCqJLSt6Trr9oC8Cy2LRc68jlJRISHdoJnqYJ8dZ4NcrdO2i8AKUu3XzUDhvg==@nickstestmongodb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();


            var cancellationToken = new CancellationToken();

            var Db = new GamesDbAccess(configuration);

            var Builder = new SeedTransformationStrategy(9);

            var buildingboard = new Dictionary<int, NodeModel>();
            var oldstyle = Builder.GeneratePuzzleFromSeed("Easy");

            foreach (var temp in oldstyle)
            {
                var Node = new NodeModel()
                {
                    Value = temp.Value,
                    PossibleValues = new List<int>()
                };
                buildingboard.Add(temp.Key, Node);
            }

            var Game = new GameModel()
            {
                Board = buildingboard,
                CompletedMoves = new Dictionary<int, MoveModel>(),
                PlayerId = new Guid(PlayerId)
            };

            var Response = await Db.CreateGame(Game, cancellationToken);
            Assert.True(Response);

        }
        [Theory]
        [InlineData("76719b91-d77e-4ce7-a02d-3b92a1202015")]
        [InlineData("c9d004da-0bf6-4cc7-a679-935b2e284f9e")]
        public async Task Read(string GameId)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://nickstestmongodb:kbpIJs6Kmj0viH8exEbRucTwWuCqJLSt6Trr9oC8Cy2LRc68jlJRISHdoJnqYJ8dZ4NcrdO2i8AKUu3XzUDhvg==@nickstestmongodb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var cancellationToken = new CancellationToken();

            var Db = new GamesDbAccess(configuration);
            var JsonOutput = await Db.ReadGame(new Guid(GameId), new Guid(PlayerId), cancellationToken);

            Assert.Equal(GameId, JsonOutput.GameId.ToString());
            Assert.Equal(PlayerId, JsonOutput.PlayerId.ToString());
        }
        [Theory]
        [InlineData("76719b91-d77e-4ce7-a02d-3b92a1202015")]
        [InlineData("c9d004da-0bf6-4cc7-a679-935b2e284f9e")]
        public async Task Update(string gameId)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://nickstestmongodb:kbpIJs6Kmj0viH8exEbRucTwWuCqJLSt6Trr9oC8Cy2LRc68jlJRISHdoJnqYJ8dZ4NcrdO2i8AKUu3XzUDhvg==@nickstestmongodb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var cancellationToken = new CancellationToken();

            var Db = new GamesDbAccess(configuration);
            GameModel Game = await Db.ReadGame(new Guid(gameId), new Guid(PlayerId), cancellationToken);

            Game.CompletedMoves.Add(Game.CompletedMoves.Count, new MoveModel() { Node = Game.CompletedMoves.Count, Entry = -1 });

            bool Result = await Db.UpdateGame(new Guid(gameId), new Guid(PlayerId), Game, cancellationToken);
            Assert.True(Result);
        }
        [Fact]
        public async Task Delete()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://nickstestmongodb:kbpIJs6Kmj0viH8exEbRucTwWuCqJLSt6Trr9oC8Cy2LRc68jlJRISHdoJnqYJ8dZ4NcrdO2i8AKUu3XzUDhvg==@nickstestmongodb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var cancellationToken = new CancellationToken();

            var Db = new GamesDbAccess(configuration);

            var Builder = new SeedTransformationStrategy(9);

            var buildingboard = new Dictionary<int, NodeModel>();
            var oldstyle = Builder.GeneratePuzzleFromSeed("Easy");

            foreach (var temp in oldstyle)
            {
                var Node = new NodeModel()
                {
                    Value = temp.Value,
                    PossibleValues = new List<int>()
                };
                buildingboard.Add(temp.Key, Node);
            }

            var Game = new GameModel()
            {
                Board = buildingboard,
                CompletedMoves = new Dictionary<int, MoveModel>(),
                PlayerId = new Guid(PlayerId)
            };

            var Response = await Db.CreateGame(Game, cancellationToken);

            var DelResponse = await Db.DeleteGame(Game.GameId, cancellationToken);

            Assert.True(DelResponse);
        }
    }
}
