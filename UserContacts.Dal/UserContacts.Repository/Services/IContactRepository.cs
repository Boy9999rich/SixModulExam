using UserContacts.Dal.Entities;

namespace UserContacts.Repository.Services
{
    public interface IContactRepository
    {
        Task<long> AddContactAsync(Contacts contact);
        Task<Contacts> GetContactByIdAsync(long contactId, long userId);
        Task<List<Contacts>> GetAllContactsAsync(long userId);
        Task DeleteContactAsync(long contactId, long userId);
        Task UpdateContactAsync(Contacts contact);
    }
}