using UserContacts.Dal.Entities;

namespace UserContacts.Repository.Services
{
    public interface IUserRepository
    {
        Task<long> InsertUserAsync(Users users);
        Task DeleteUserByIdAsync(long Id);
        Task<Users> SelectUserByIdAsync(long Id);
        Task<Users> SelectUserByUserNameAsync(string userName);
        Task UpdateUserRoleAsync(long userId, string userRole);
        Task<bool> CheckUserById(long userId);
        Task<bool> CheckUsernameExists(string username);
        Task<bool> CheckEmailExists(string email);
        Task<bool> CheckPhoneNumberExists(string phoneNum);
    }
}