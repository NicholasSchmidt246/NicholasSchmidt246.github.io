using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Sudoku_WebService.DataAccess;
using Sudoku_WebService.Formatters;
using Sudoku_WebService.Models;

namespace Sudoku_WebService.Strategies
{
    public class AuthorizationStrategy : IDisposable
    {
        private IConfiguration Config;

        #region Constructors

        public AuthorizationStrategy(IConfiguration config)
        {
            Config = config;
        }

        #endregion

        #region UserData Manipulators

        private async Task<Guid> AddUserData(ContentTypeTransformer.UnifiedContentType contentType, string userData, CancellationToken cancellationToken)
        {
            if (!UserInputValidationStrategy.ValidateUserData(userData))
            {
                throw new ArgumentException();
            }

            var Player = ContentTypeTransformer.CreateObjectFromContent<PlayerModel>(contentType, userData);

            var PlayerDb = new PlayersDbAccess(Config);
            bool Success = await PlayerDb.CreatePlayer(Player, cancellationToken);

            if (!Success)
            {
                // Throw Exception
            }

            var UserId = await PlayerDb.GetPlayerId(Player.Email, cancellationToken);

            return UserId;
        }
        private async Task<bool> DeleteUserData(Guid userId, CancellationToken cancellationToken)
        {
            // Normally, this would be restricted to the calling user only. Since we do not have a real auth system in place that will not be the case here.
            // Trust delete, if this is to ever be a real product, this should be addressed

            var PlayerDb = new PlayersDbAccess(Config);
            bool Success = await PlayerDb.DeletePlayer(userId, cancellationToken);

            return Success;
        }
        private async Task<PlayerModel> GetUserData(Guid userId, CancellationToken cancellationToken)
        {
            var PlayerDb = new PlayersDbAccess(Config);
            var Player = await PlayerDb.ReadPlayer(userId, cancellationToken);

            return Player;
        }
        private async Task<bool> UpdateUserData(ContentTypeTransformer.UnifiedContentType contentType, string userData, CancellationToken cancellationToken)
        {
            if (!UserInputValidationStrategy.ValidateUserData(userData))
            {
                throw new ArgumentException();
            }

            var Player = ContentTypeTransformer.CreateObjectFromContent<PlayerModel>(contentType, userData);

            var PlayerDb = new PlayersDbAccess(Config);
            bool Success = await PlayerDb.UpdatePlayer(Player, cancellationToken);

            return Success;
        }

        #endregion
        #region PlayersController Interface

        // These methods are just decorators of our internal methods and should not be doing any work outside of that.

        public async Task<Stream> AddUserData(string inputContentType, string outputContentType, string userData, CancellationToken cancellationToken)
        {
            var inContentType = ContentTypeTransformer.UnifyContentType(inputContentType);
            var outContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            Guid userId = await AddUserData(inContentType, userData, cancellationToken);

            var AddUserDataResponse = new Dictionary<string, Guid>()
            {
                { "User Id", userId }
            };

            return ContentTypeTransformer.FormatContent(outContentType, AddUserDataResponse);
        }
        public async Task<Stream> DeleteUserData(string outputContentType, Guid userId, CancellationToken cancellationToken)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            bool DeleteSuccessful = await DeleteUserData(userId, cancellationToken);

            var DeleteUserDataResponse = new Dictionary<string, bool>
            {
                { "Delete Successful", DeleteSuccessful }
            };

            return ContentTypeTransformer.FormatContent(ContentType, DeleteUserDataResponse);
        }
        public async Task<Stream> GetUserData(string outputContentType, Guid userId, CancellationToken cancellationToken)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            PlayerModel UserData = await GetUserData(userId, cancellationToken);

            return ContentTypeTransformer.FormatContent(ContentType, UserData);
        }
        public async Task<Stream> UpdateUserData(string inputContentType, string outputContentType, string userData, CancellationToken cancellationToken)
        {
            var inContentType = ContentTypeTransformer.UnifyContentType(inputContentType);
            var outContentType = ContentTypeTransformer.UnifyContentType(outputContentType);

            bool UpdateSuccessful = await UpdateUserData(inContentType, userData, cancellationToken);

            var UpdateUserDataResponse = new Dictionary<string, bool>
            {
                { "Update Successful", UpdateSuccessful }
            };

            return ContentTypeTransformer.FormatContent(outContentType, UpdateUserDataResponse);
        }

        #endregion

        public void Dispose()
        {
            Config = null;
        }
    }
}
