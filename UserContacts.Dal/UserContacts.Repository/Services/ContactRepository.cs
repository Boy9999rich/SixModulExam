using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserContacts.Dal;
using UserContacts.Dal.Entities;

namespace UserContacts.Repository.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly MainContext _MainContext;

        public ContactRepository(MainContext mainContext)
        {
            _MainContext = mainContext;
        }

        public async Task<long> AddContactAsync(Contacts contact)
        {
            await _MainContext.Contacts.AddAsync(contact);
            await _MainContext.SaveChangesAsync();
            return contact.Id;
        }

        public async Task DeleteContactAsync(long contactId, long userId)
        {
            var contact = await GetContactByIdAsync(contactId, userId);
            _MainContext.Contacts.Remove(contact);
            await _MainContext.SaveChangesAsync();
        }

        public async Task<List<Contacts>> GetAllContactsAsync(long userId)
        {
            return await _MainContext.Contacts.Where(_ => _.UserId == userId).ToListAsync();
        }

        public async Task<Contacts> GetContactByIdAsync(long contactId, long userId)
        {
            var contact = await _MainContext.Contacts.FirstOrDefaultAsync(b => b.Id == contactId);
            if (contact.UserId != userId)
            {
                throw new Exception($"not found {contact}");
            }
            return contact;
        }

        public async Task UpdateContactAsync(Contacts contact)
        {
            _MainContext.Contacts.Update(contact);
            await _MainContext.SaveChangesAsync();
        }
    }
}
