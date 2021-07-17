using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManager.DAL.Entities;
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
                    GlobalUsers.UsersCollection = await userInfoRepository.GetUsersAsync();

                    foreach (UserInfo u in GlobalUsers.UsersCollection)
                    {
                        _logger.LogInformation($"{u.ID}.{u.Name} - {u.Status}");
                    }
                    _logger.LogInformation("***");

                    await Task.Delay(TimeSpan.FromSeconds(2));
                }
            }

        }



    }
}