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
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo(int Id)
        {
            UserInfo user;

            user = Globals.Users.Find(u => u.ID == Id);

            if (user == null)
            {
                user = await _userInfoService.GetUserInfo(Id);
            }

            return View(user);
        }

        [HttpGet]
        public List<UserInfo> GetUsers()
        {
            return Globals.Users;
        }
    }
}
