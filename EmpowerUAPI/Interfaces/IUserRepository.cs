using EmpowerEFCore.Domain;

namespace EmpowerUAPI.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> UserExistsByEmail(string email);
        Task<User?> GetUserByEmail(string email);
    }
}
