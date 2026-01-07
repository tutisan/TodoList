
namespace TodoList.DTOs;

public class AccountCreateDTO(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}
