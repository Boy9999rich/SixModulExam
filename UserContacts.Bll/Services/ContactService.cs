using FluentValidation;
using UserContacts.Bll.Dtos;
using UserContacts.Core.Errors;
using UserContacts.Dal.Entities;
//using UserContacts.Dal.UserContacts.Repository.Services;
namespace UserContacts.Bll.Services;

public class ContactService(Repository.Services.IContactRepository ContactRepository, IValidator<ContactCreateDto> CreateDtoValidator,
    IValidator<ContactDto> UpdateDtoValidator) : IContactService
{

    private Contacts Converter(ContactCreateDto contactCreateDto)
    {
        return new Contacts
        {
            Address = contactCreateDto.Address,
            Email = contactCreateDto.Email,
            FirstName = contactCreateDto.FirstName,
            LastName = contactCreateDto.LastName,
            PhoneNumber = contactCreateDto.PhoneNumber,
        };
    }
    private ContactDto Converter(Contacts contact)
    {
        return new ContactDto
        {
            Address = contact.Address,
            Email = contact.Email,
            FirstName = contact.FirstName,
            Id = contact.Id,
            PhoneNumber = contact.PhoneNumber,
            LastName = contact.LastName,


        };
    }

    public async Task<long> AddContactAsync(ContactCreateDto contactCreateDto, long userId)
    {
        var res = CreateDtoValidator.Validate(contactCreateDto);
        if (!res.IsValid)
        {
            string errorMessages = string.Join("; ", res.Errors.Select(e => e.ErrorMessage));
            throw new NotAllowedException(errorMessages);
        }
        var contactEntity = Converter(contactCreateDto);
        contactEntity.UserId = userId;
        contactEntity.CreatedAt = DateTime.UtcNow;
        return await ContactRepository.AddContactAsync(contactEntity);
    }

    public async Task DeleteContactAsync(long contactId, long userId)
    {
        await ContactRepository.DeleteContactAsync(contactId, userId);
    }

    public async Task<List<ContactDto>> GetAllContactsAsync(long userId)
    {
        var contacts = await ContactRepository.GetAllContactsAsync(userId);
        return contacts.Select(_ => Converter(_)).ToList();
    }

    public async Task<ContactDto> GetContactByIdAsync(long contactId, long userId)
    {
        var contact = await ContactRepository.GetContactByIdAsync(contactId, userId);
        return Converter(contact);
    }

    public async Task UpdateContactAsync(ContactDto contactDto, long userId)
    {
        var res = UpdateDtoValidator.Validate(contactDto);
        if (!res.IsValid)
        {
            string errorMessages = string.Join("; ", res.Errors.Select(e => e.ErrorMessage));
            throw new NotAllowedException(errorMessages);
        }
        var contact = await ContactRepository.GetContactByIdAsync(contactDto.Id, userId);
        contact.Email = contactDto.Email;
        contact.FirstName = contactDto.FirstName;
        contact.LastName = contactDto.LastName;
        contact.PhoneNumber = contactDto.PhoneNumber;
        contact.Address = contactDto.Address;
        await ContactRepository.UpdateContactAsync(contact);   
    }
}
