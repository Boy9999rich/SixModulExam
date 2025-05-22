using Microsoft.EntityFrameworkCore;
using UserContacts.Dal;
using UserContacts.Dal.Entities;

namespace UserContacts.Repository.Services;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly MainContext _MainContext;

    public RefreshTokenRepository(MainContext mainContext)
    {
        _MainContext = mainContext;
    }

    public async Task AddRefreshToken(RefreshTokens refreshToken)
    {
        await _MainContext.RefreshTokens.AddAsync(refreshToken);
        await _MainContext.SaveChangesAsync();
    }

    public async Task<RefreshTokens> SelectRefreshToken(string refreshToken, long userId)
    {
        Task<RefreshTokens?> task = _MainContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.UserId == userId);
        return await task;
    }
}
