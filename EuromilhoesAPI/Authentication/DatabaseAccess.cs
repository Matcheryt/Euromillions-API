using Microsoft.Extensions.Configuration;

namespace EuromilhoesAPI.Authentication
{
    public class DatabaseAccess
    {
        private readonly IConfiguration _config;

        public DatabaseAccess(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Checks whether specified token has permission to access database.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns><see langword="true"/> if token has permission to access database, otherwise returns <see langword="false"/>.</returns>
        public bool HasAccess(string token)
        {
            return token == _config["DbAccessToken"];
        }
    }
}
