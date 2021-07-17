using System.Threading.Tasks;
using UserManager.BL.Models;
using UserManager.DAL.Entities;

namespace UserManager.BL.Services
{
    public interface IUserInfoService
    {
        Task<UserInfo> GetUserInfo(int id);
        Task<Response> CreateUserAsync(UserInfo userInfo);
        Task<RemoveUserResponse> RemoveUserAsync(RemoveUserRequest removeUserRequest);
        Task<SetStatusResponse> SetStatusAsync(SetStatusRequest setStatusRequest);
    }
}