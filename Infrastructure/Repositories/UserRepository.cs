using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(SampleShopV2Context context) : base(context) { }

        public async Task<User?> GetUserByLoginAsync(string userName, string password)
        {
            return await _context.Set<User>()
                .FirstOrDefaultAsync(u => (u.UserName == userName) && (u.Password == password));
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _context.Set<User>()
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}