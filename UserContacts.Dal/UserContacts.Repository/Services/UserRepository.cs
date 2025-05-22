using Microsoft.EntityFrameworkCore;
//using Umbraco.Core.Persistence;
using UserContacts.Dal;
using UserContacts.Dal.Entities;


namespace UserContacts.Repository.Services;

public class UserRepository : IUserRepository
{
    private readonly MainContext _MainContext;

    public UserRepository(MainContext mainContext)
    {
        _MainContext = mainContext;
    }

    public async Task<bool> CheckEmailExists(string email)
    {
        return await _MainContext.Users.AnyAsync(_ => _.Email == email);
    }

    public async Task<bool> CheckPhoneNumberExists(string phoneNum)
    {
        return await _MainContext.Users.AnyAsync(b => b.PhoneNumber == phoneNum);
    }

    public async Task<bool> CheckUserById(long userId)
    {
        return await _MainContext.Users.AnyAsync(b => b.UserId == userId);
    }

    public async Task<bool> CheckUsernameExists(string username)
    {
        return await _MainContext.Users.AnyAsync(b => b.UserName == username);
    }

    public async Task DeleteUserByIdAsync(long Id)
    {
        var user = await SelectUserByIdAsync(Id);
        _MainContext.Users.Remove(user);
        await _MainContext.SaveChangesAsync();
    }

    public async Task<long> InsertUserAsync(Users users)
    {
        _MainContext.Users.AddAsync(users);
        await _MainContext.SaveChangesAsync();
        return users.UserId;
    }

    public async Task<Users> SelectUserByIdAsync(long Id)
    {
        var user = await _MainContext.Users.Include(_ => _.Role).FirstOrDefaultAsync(x => x.UserId == Id);
        if (user == null)
        {
            throw new Exception($"Entity with {Id} not found");
        }
        return user;
    }

    public async Task<Users> SelectUserByUserNameAsync(string userName)
    {
        var user = await _MainContext.Users.Include(b => b.Role).FirstOrDefaultAsync(b => b.UserName == userName);
        if (user is null)
        {
            throw new Exception("not found Id");
        }
        return user;
    }

    public async Task UpdateUserRoleAsync(long userId, string userRole)
    {
        var user = await SelectUserByIdAsync(userId);
        var role = await _MainContext.UserRole.FirstOrDefaultAsync(b => b.Name == userRole);
        if (role is null)
        {
            throw new Exception($"not found {userRole} ");
        }
        user.RoleId = role.Id;
        _MainContext.Users.Update(user);
        await _MainContext.SaveChangesAsync();
    }
}
