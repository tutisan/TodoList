
namespace TodoList.Models;

public class Account(string username, byte[] passwordHash, byte[] passwordSalt)
{
    public Guid AccountId { get; set; }
    public string Username { get; set; } = username;
    public byte[] PasswordHash { get; set; } = passwordHash;
    public byte[] PasswordSalt { get; set; } = passwordSalt;
}
