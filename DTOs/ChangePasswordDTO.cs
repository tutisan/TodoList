
namespace TodoList.DTOs;

public class ChangePasswordDTO(string password, string passwordConfirm)
{
    public string Password { get; set; } = password;
    public string PasswordConfirm { get; set; } = passwordConfirm;
}
