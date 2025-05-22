using UserContacts.Dal.Entities;

namespace UserContacts.Repository.Services
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshToken(RefreshTokens refreshToken);
        Task<RefreshTokens> SelectRefreshToken(string refreshToken, long userId);
    }
}