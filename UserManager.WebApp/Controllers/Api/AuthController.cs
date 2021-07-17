using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UserManager.BL.Models;
using UserManager.BL.Services;
using UserManager.DAL.Entities;
using UserManager.WebApp.Infrastructure.BasicAuth;

namespace UserManager.WebApp.Controllers.Api
{
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly ILogger<AuthController> _logger;
        private readonly IUserInfoService _userInfoService;

        public AuthController(
            ILogger<AuthController> logger,
            IUserInfoService userInfoService)
        {
            _logger = logger;
            _userInfoService = userInfoService;
        }


        [BasicAuth]
        [HttpPost]
        [Route("Auth/CreateUser")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public async Task<Response> CreateUserAsync(UserInfo userInfo)
        {
            return await _userInfoService.CreateUserAsync(userInfo);
        }

        [BasicAuth]
        [HttpPost]
        [Route("Auth/RemoveUser")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<RemoveUserResponse> RemoveUserAsync([FromBody] RemoveUserRequest RemoveUserRequest)
        {
            return await _userInfoService.RemoveUserAsync(RemoveUserRequest);
        }

        [BasicAuth]
        [HttpPost]
        [Route("Auth/SetStatus")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<SetStatusResponse> SetStatusAsync([FromForm] SetStatusRequest setStatusRequest)
        {
            return await _userInfoService.SetStatusAsync(setStatusRequest);
        }

    }
}