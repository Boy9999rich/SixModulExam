namespace UserContacts.Dal.Entities;

public class Users
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Salt { get; set; }

    public long RoleId { get; set; }
    public UserRole Role { get; set; }

    public ICollection<Contacts> Contacts { get; set; }
    public ICollection<RefreshTokens> RefreshTokens { get; set; }
}
