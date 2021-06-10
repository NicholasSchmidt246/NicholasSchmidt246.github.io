using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Sudoku_WebService.Strategies
{
    public class AuthorizationStrategy : IDisposable
    {
        public Guid UserId;

        #region Constructors

        public AuthorizationStrategy(Guid userId)
        {
            UserId = userId;
        }

        #endregion

        // This method does not belong in UserInputValidationStrategy as it cannot validate without the Db
        // Only checks authorization to access service, not authorization to the puzzleId they are seeking
        public async Task<bool> IsAuthorized(Guid userId)
        {
            bool isAuthorized = false;

            await Task.Delay(0);
            // TODO: Validate userId

            return isAuthorized;
        }

        #region UserData Manipulators

        private static async Task<Guid> AddUserData(string userData)
        {
            if (!UserInputValidationStrategy.ValidateUserData(userData))
            {
                throw new ArgumentException();
            }

            // TODO: Submit data to Db
            // TODO: Assign value to UserId
            await Task.Delay(0);

            return Guid.Empty;
        }
        private async Task<bool> DeleteUserData()
        {
            await IsAuthorized(UserId);

            await Task.Delay(0);
            // TODO: Delete userData for userId

            return true;
        }
        private async Task<string> GetUserData()
        {
            await IsAuthorized(UserId);

            await Task.Delay(0);
            // TODO: get userData for userId
            // TODO: return userData

            return string.Empty;
        }
        private async Task<bool> UpdateUserData(string userData)
        {
            await IsAuthorized(UserId);

            if(!UserInputValidationStrategy.ValidateUserData(userData))
            {
                throw new ArgumentException();
            }

            await Task.Delay(0);
            // TODO: Submit userData to mongo for userId

            return true;
        }

        #endregion
        #region PlayersController Interface

        // These methods are just decorators of our internal methods and should not be doing any work outside of that.

        public static async Task<Stream> AddUserData(string contentType, string userData) // TODO: make an actual object for userData
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

            Guid userId = await AddUserData(userData);

            var AddUserDataResponse = new Dictionary<string, Guid>()
            {
                { "User Id", userId }
            };

            return ContentTypeTransformer.FormatContent(ContentType, AddUserDataResponse);
        }
        public async Task<Stream> DeleteUserData(string contentType)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

            bool DeleteSuccessful = await DeleteUserData();

            var DeleteUserDataResponse = new Dictionary<string, bool>
            {
                { "Delete Successful", DeleteSuccessful }
            };

            return ContentTypeTransformer.FormatContent(ContentType, DeleteUserDataResponse);
        }
        public async Task<Stream> GetUserData(string contentType)
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

            string UserData = await GetUserData();

            return ContentTypeTransformer.FormatContent(ContentType, UserData);
        }
        public async Task<Stream> UpdateUserData(string contentType, string userData) // TODO: make an actual object for userData
        {
            var ContentType = ContentTypeTransformer.UnifyContentType(contentType);

            bool UpdateSuccessful = await UpdateUserData(userData);

            var UpdateUserDataResponse = new Dictionary<string, bool>
            {
                { "Update Successful", UpdateSuccessful }
            };

            return ContentTypeTransformer.FormatContent(ContentType, UpdateUserDataResponse);
        }

        #endregion

        public void Dispose()
        {
            UserId = Guid.Empty;
        }
    }
}
