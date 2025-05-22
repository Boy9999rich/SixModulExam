using Microsoft.EntityFrameworkCore;
//using Umbraco.Core.Persistence;
using UserContacts.Dal;
using UserContacts.Dal.Entities;

namespace UserContacts.Repository.Services;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly MainContext _MainContext;

    public UserRoleRepository(MainContext mainContext)
    {
        _MainContext = mainContext;
    }

    public async Task<List<UserRole>> GetAllRolesAsync()
    {
        return await _MainContext.UserRole.ToListAsync();
    }

    public async Task<ICollection<Users>> GetAllUsersByRoleAsync(string role)
    {
        var foundRole = await _MainContext.UserRole.Include(b => b.Users).FirstOrDefaultAsync(b => b.Name == role);
        if (foundRole is null)
        {
            throw new Exception($"not found {foundRole}");
        }
        return foundRole.Users;
    }

    public async Task<long> GetRoleIdAsync(string role)
    {
        var foundRole = await _MainContext.UserRole.FirstOrDefaultAsync(b => b.Name == role);
        if (foundRole is null)
        {
            throw new Exception("not found");
        }
        return foundRole.Id;
    }
}
