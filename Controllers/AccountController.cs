using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    #region Endpoints
    [HttpPost("register")]
    public IActionResult CreateNewAccount(string username, string password1, string password2)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("login")]
    public IActionResult Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    [HttpPut("change_password")]
    public IActionResult UpdatePassword(string newPassword)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("delete")]
    public IActionResult DeleteAccount()
    {
        throw new NotImplementedException();
    }
    #endregion
}
