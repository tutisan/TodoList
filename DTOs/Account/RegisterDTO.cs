
namespace TodoList.DTOs;

public class RegisterDTO(string username, string password)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
}
