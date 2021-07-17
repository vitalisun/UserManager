using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UserManager.WebApp.Services
{
    public interface IUserService
    {
        bool IsValidUser(string userName, string password);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;

        public UserService(ILogger<UserService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public bool IsValidUser(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");

            if (string.IsNullOrWhiteSpace(userName) || userName != _configuration["Credentials:Login"])
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password) || password != _configuration["Credentials:Password"])
            {
                return false;
            }
            return true;
        }
    }
}