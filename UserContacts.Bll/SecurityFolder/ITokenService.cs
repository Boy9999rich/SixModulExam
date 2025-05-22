using System.Security.Claims;
using UserContacts.Bll.Dtos;

namespace UserContacts.Bll.Security
{
    public interface ITokenService
    {
        public string GenerateToken(UserGetDto user);
        public string GenerateRefreshToken();
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}