using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManager.DAL.Repository;

namespace UserManager.WebApp.Services
{
    public class GlobalUsersService : BackgroundService
    {
        private readonly ILogger _logger;

        public GlobalUsersService(
            ILogger<GlobalUsersService> logger,
            IServiceProvider services)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await UpdateGlobalUsers(stoppingToken);
        }


        public async Task UpdateGlobalUsers(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var userInfoRepository = scope.ServiceProvider.GetRequiredService<IUserInfoRepository>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    Globals.Users = await userInfoRepository.GetUsersAsync();

                    _logger.LogInformation("UsersCollection was updated");

                    await Task.Delay(TimeSpan.FromMinutes(10));
                }
            }

        }



    }
}