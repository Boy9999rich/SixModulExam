using UserContacts.Bll.Dtos;
using UserContacts.Dal.Entities;
//using UserContacts.Repository.Services;

namespace UserContacts.Bll.Services;

public class RoleService(Repository.Services.IUserRoleRepository UserRoleRepository) : IRoleService
{
    private RoleGetDto Converter(UserRole role)
    {
        return new RoleGetDto
        {
            Description = role.Description,
            Id = role.Id,
            Name = role.Name,
        };
    }

    private UserGetDto Converter(Users user)
    {
        return new UserGetDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserId = user.UserId,
            UserName = user.UserName,
            Role = user.Role.Name,
        };
    }
    public async Task<List<RoleGetDto>> GetAllRolesAsync()
    {
        var roles = await UserRoleRepository.GetAllRolesAsync();
        return roles.Select(Converter).ToList();
    }

    public async Task<long> GetRoleIdAsync(string role)
    {
        return await UserRoleRepository.GetRoleIdAsync(role);
    }

    public async Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role)
    {
        var users = await UserRoleRepository.GetAllUsersByRoleAsync(role);
        return users.Select(Converter).ToList();
    }

    public async Task<long> AddRole(UserRole userRole)
    {
        return await UserRoleRepository.AddRoleAsync(userRole);
    }
}
