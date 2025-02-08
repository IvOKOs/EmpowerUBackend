using EmpowerEFCore.Data;
using EmpowerEFCore.Domain;
using EmpowerUAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmpowerUAPI.Services
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly EmpowerUDbContext _dbContext;

        public UserRepository(EmpowerUDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            var user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user != null;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}
