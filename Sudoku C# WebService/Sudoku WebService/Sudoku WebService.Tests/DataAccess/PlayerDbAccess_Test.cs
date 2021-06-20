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

namespace Sudoku_WebService.Tests.DataAccess
{
    public class PlayerDbAccess_Test
    {
        private Tuple<string, string> TestPairOne = new Tuple<string, string>("Test1@Test.com", "43214893-3865-43d0-8287-c38c8c971fc0");
        private Tuple<string, string> TestPairTwo = new Tuple<string, string>("Test2@Test.com", "a76ed7bb-1c4d-494e-901b-a20aaea01cd6");

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetPlayerId_SuccessCase(int testPair)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://nickstestmongodb:kbpIJs6Kmj0viH8exEbRucTwWuCqJLSt6Trr9oC8Cy2LRc68jlJRISHdoJnqYJ8dZ4NcrdO2i8AKUu3XzUDhvg==@nickstestmongodb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();


            var cancellationToken = new CancellationToken();

            Tuple<string, string> TestPair;
            switch (testPair)
            {

                case 1:
                    TestPair = TestPairOne;
                    break;
                case 2:
                    TestPair = TestPairTwo;
                    break;
                default:
                    TestPair = null;
                    break;
            }

            var Db = new PlayersDbAccess(configuration);
            var Id = await Db.GetPlayerId(TestPair.Item1, cancellationToken);

            Assert.Equal(new Guid(TestPair.Item2), Id);
        }
        [Theory]
        [InlineData("Test3@Test.com")]
        [InlineData("Test4@Test.com")]
        public async Task Create_SuccessCase(string email)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://nickstestmongodb:kbpIJs6Kmj0viH8exEbRucTwWuCqJLSt6Trr9oC8Cy2LRc68jlJRISHdoJnqYJ8dZ4NcrdO2i8AKUu3XzUDhvg==@nickstestmongodb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();


            var cancellationToken = new CancellationToken();

            var Db = new PlayersDbAccess(configuration);
            await Db.CreatePlayer(email, "Automated Test", cancellationToken);

            // Delete Db Records as cleanup
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task ReadSuccessCase(int testPair)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://nickstestmongodb:kbpIJs6Kmj0viH8exEbRucTwWuCqJLSt6Trr9oC8Cy2LRc68jlJRISHdoJnqYJ8dZ4NcrdO2i8AKUu3XzUDhvg==@nickstestmongodb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();


            var cancellationToken = new CancellationToken();

            Tuple<string, string> TestPair;
            switch (testPair)
            {

                case 1:
                    TestPair = TestPairOne;
                    break;
                case 2:
                    TestPair = TestPairTwo;
                    break;
                default:
                    TestPair = null;
                    break;
            }

            var Db = new PlayersDbAccess(configuration);
            var JsonOutput = await Db.ReadPlayer(new Guid(TestPair.Item2), cancellationToken);

            Assert.Equal(TestPair.Item1, JsonOutput.Email);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task Update(int testPair)
        {
            var myConfiguration = new Dictionary<string, string>
            {
                { "ConnectionStrings:MongoDb", "mongodb://nickstestmongodb:kbpIJs6Kmj0viH8exEbRucTwWuCqJLSt6Trr9oC8Cy2LRc68jlJRISHdoJnqYJ8dZ4NcrdO2i8AKUu3XzUDhvg==@nickstestmongodb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();


            var cancellationToken = new CancellationToken();

            Tuple<string, string> TestPair;
            switch (testPair)
            {

                case 1:
                    TestPair = TestPairOne;
                    break;
                case 2:
                    TestPair = TestPairTwo;
                    break;
                default:
                    TestPair = null;
                    break;
            }

            var Db = new PlayersDbAccess(configuration);
            var JsonOutput = await Db.ReadPlayer(new Guid(TestPair.Item2), cancellationToken);

            string UpdatedUserName = string.Empty;
            switch (JsonOutput.UserName)
            {
                case "Automated Test":
                    UpdatedUserName = "Updated Automated Test";
                    break;
                case "Updated Automated Test":
                    UpdatedUserName = "Automated Test";
                    break;
                default:
                    break;
            }

            var UpdateResult = await Db.UpdatePlayer(new Guid(TestPair.Item2), TestPair.Item1, UpdatedUserName, cancellationToken);

            Assert.True(UpdateResult);

            JsonOutput = await Db.ReadPlayer(new Guid(TestPair.Item2), cancellationToken);
            Assert.Equal(UpdatedUserName, JsonOutput.UserName);
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

            var Db = new PlayersDbAccess(configuration);

            string Email = "Test@Test.com";

            if (!await Db.PlayerExists(Email, cancellationToken))
            {
                var isCreated = await Db.CreatePlayer(Email, "Automated Test", cancellationToken);

                Assert.True(isCreated);
            }

            Guid Id = await Db.GetPlayerId(Email, cancellationToken);

            var isDeleted = await Db.DeletePlayer(Id, cancellationToken);

            Assert.True(isDeleted);
        }
    }
}
