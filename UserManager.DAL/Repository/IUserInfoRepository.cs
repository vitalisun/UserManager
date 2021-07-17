using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.DAL.Entities;

namespace UserManager.DAL.Repository
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> GetById(int id);
        Task<List<UserInfo>> GetUsersAsync();
        Task InsertAsync(UserInfo userInfo);
        Task DeleteAsync(UserInfo userInfo);
        Task UpdateAsync(UserInfo userInfo);
    }
}