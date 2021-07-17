using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.DAL.Entities;

namespace UserManager.DAL.Repository.Implementation
{
    public class UserInfoRepository : IUserInfoRepository
    {

        private readonly ApplicationContext _context;

        public UserInfoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserInfo> GetById(int id)
        {
            return await _context.UserInfo.SingleOrDefaultAsync(u => u.ID == id);
        }

        public async Task<List<UserInfo>> GetUsersAsync()
        {
            return await _context.UserInfo.ToListAsync();
        }

        public async Task InsertAsync(UserInfo userInfo)
        {
            await _context.UserInfo.AddAsync(userInfo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserInfo userInfo)
        {
            _context.UserInfo.Remove(userInfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserInfo userInfo)
        {
            _context.Entry(userInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
