using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using TodoList.Data;
using TodoList.DTOs;
using TodoList.Models;
using TodoList.Services;

namespace TodoList.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    #region Controller setup
    private readonly TodoListDbContext _dbContext;

    public AccountController()
    {
        _dbContext = new TodoListDbContext();
    }
    #endregion

    #region Debug endpoints
    [HttpGet("debug/list_users")]
    public IActionResult ListUserAndPasswords()
    {
        return Ok(_dbContext.Accounts.ToList());
    }

    [HttpPost("debug/validate_login")]
    public IActionResult ValidateLogin(AccountCreateDTO createDTO)
    {
        var user = _dbContext.Accounts.FirstOrDefault(u => u.Username == createDTO.Username);

        if (user != null)
        {
            byte[] passwordHash = KeyDerivation.Pbkdf2(
                createDTO.Password,
                user.PasswordSalt,
                KeyDerivationPrf.HMACSHA256,
                user.PasswordIterationCount,
                256/8
            );
            
            if (user.PasswordHash.SequenceEqual(passwordHash))
            {
                return Ok("Login is valid.");
            }

            return Ok("Login is invalid.");
        }

        return Ok("User not found.");
    }
    #endregion

    #region Endpoints
    [HttpPost("register")]
    public IActionResult CreateNewAccount(AccountCreateDTO newAccountDTO)
    {
        byte[] passwordSalt = RandomNumberGenerator.GetBytes(128 / 8);
        byte[] passWordHash = KeyDerivation.Pbkdf2(newAccountDTO.Password, passwordSalt, KeyDerivationPrf.HMACSHA256, 100_000, 256/8);

        Account newAccount = new Account(newAccountDTO.Username, passWordHash, passwordSalt);
        newAccount.PasswordIterationCount = 100_000;
        _dbContext.Accounts.Add(newAccount);
        _dbContext.SaveChanges();
        return Ok();
    }
    
    [HttpPost("login")]
    public IActionResult Login(AccountCreateDTO login)
    {
        var user = _dbContext.Accounts.FirstOrDefault(u => u.Username == login.Username);

        if (user != null)
        {
            byte[] passwordHash = KeyDerivation.Pbkdf2(
                login.Password,
                user.PasswordSalt,
                KeyDerivationPrf.HMACSHA256,
                user.PasswordIterationCount,
                256/8
            );

            if (user.PasswordHash.SequenceEqual(passwordHash))
            {
                return Ok(AuthService.GenerateJWT(user));
            }
        }

        return Unauthorized();
    }

    [Authorize]
    [HttpPut("change_password")]
    public IActionResult UpdatePassword(AccountChangePasswordDTO changePassword)
    {
        var loggedUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = _dbContext.Accounts.First(u => u.Username == loggedUser);

        if (changePassword.Password == changePassword.PasswordConfirm)
        {
            byte[] passwordSalt = RandomNumberGenerator.GetBytes(128 / 8);
            byte[] passwordHash = KeyDerivation.Pbkdf2(changePassword.Password, passwordSalt, KeyDerivationPrf.HMACSHA256, 100_000, 256/8);

            user.PasswordIterationCount = 100_000;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _dbContext.SaveChanges();

            return Ok("Password changed");
        }

        return Ok("Passwords are not equal");
    }

    [HttpDelete("delete")]
    public IActionResult DeleteAccount()
    {
        throw new NotImplementedException();
    }
    #endregion
}
