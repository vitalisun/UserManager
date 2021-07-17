using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BL.Services;
using UserManager.DAL.Entities;

namespace UserManager.WebApp.Controllers
{
    public class PublicController : Controller
    {
        private readonly ILogger<PublicController> _logger;
        private readonly IUserInfoService _userInfoService;

        public PublicController(
            ILogger<PublicController> logger,
            IUserInfoService userInfoService)
        {
            _logger = logger;
            _userInfoService = userInfoService;

            System.Console.WriteLine(GlobalUsers.UsersCollection);
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo(int Id)
        {
            UserInfo user;

            List<UserInfo> users = new List<UserInfo>();

            foreach (var item in GlobalUsers.UsersCollection)
            {
                users.Add(new DAL.Entities.UserInfo
                {
                    ID = item.ID,
                    Name = item.Name,
                    Status = item.Status
                });
            }

            user = users.Find(u => u.ID == Id);

            if (user == null)
            {
                user = await _userInfoService.GetUserInfo(Id);
            }

            return View(user);
        }

        [HttpGet]
        public List<UserInfo> GetUsers()
        {
            List<UserInfo> users = new List<UserInfo>();

            foreach (var item in GlobalUsers.UsersCollection)
            {
                users.Add(new DAL.Entities.UserInfo
                {
                    ID = item.ID,
                    Name = item.Name,
                    Status = item.Status
                });
            }

            return users;
        }
    }
}
