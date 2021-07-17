using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        public IActionResult UserInfo(int Id)
        {
            ViewBag.Id = Id;
            return base.View(Globals.Users.Find(u => u.ID == Id));
        }

        [HttpGet]
        public List<UserInfo> GetUsers()
        {
            return Globals.Users;
        }
    }
}
