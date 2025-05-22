namespace UserContacts.Dal.Entities;

public class RefreshTokens
{
    public long RefreshTokenId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; }

    public long UserId { get; set; }
    public Users Users { get; set; }

}
