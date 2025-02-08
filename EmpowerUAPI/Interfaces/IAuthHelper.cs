using System.Security.Claims;

namespace EmpowerUAPI.Interfaces
{
    public interface IAuthHelper
    {
        string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt);
    }
}
