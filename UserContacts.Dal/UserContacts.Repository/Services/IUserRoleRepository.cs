using UserContacts.Dal.Entities;

namespace UserContacts.Repository.Services
{
    public interface IUserRoleRepository
    {
        Task<ICollection<Users>> GetAllUsersByRoleAsync(string role);
        Task<List<UserRole>> GetAllRolesAsync();
        Task<long> GetRoleIdAsync(string role);
        Task<long> AddRoleAsync(UserRole userRole);
    }
}