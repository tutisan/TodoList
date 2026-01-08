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

readonly struct HashAndSalt
{
    public readonly int IterationCount;
    public readonly byte[] PasswordHash;
    public readonly byte[] PasswordSalt;

    public HashAndSalt(string password, int iterationCount = 100_000)
    {
        IterationCount = iterationCount;
        PasswordSalt = RandomNumberGenerator.GetBytes(128 / 8);
        PasswordHash = KeyDerivation.Pbkdf2(password, PasswordSalt, KeyDerivationPrf.HMACSHA256, IterationCount, 256 / 8);
    }
}

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

    private bool ValidatePassword(Account user, string password)
    {
        byte[] passwordHash = KeyDerivation.Pbkdf2(
            password,
            user.PasswordSalt,
            KeyDerivationPrf.HMACSHA256,
            user.PasswordIterationCount,
            256/8
        );

        return user.PasswordHash.SequenceEqual(passwordHash);
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

        if (user != null && ValidatePassword(user, login.Password))
        {
            return Ok(AuthService.GenerateJWT(user));
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
            HashAndSalt passwordInfo = new HashAndSalt(changePassword.Password);
            user.PasswordIterationCount = passwordInfo.IterationCount;
            user.PasswordHash = passwordInfo.PasswordHash;
            user.PasswordSalt = passwordInfo.PasswordSalt;
            _dbContext.SaveChanges();

            return Ok("Password changed");
        }

        return Ok("Passwords are not equal");
    }

    [Authorize]
    [HttpDelete("delete")]
    public IActionResult DeleteAccount()
    {
        var loggedUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = _dbContext.Accounts.First(u => u.Username == loggedUser);
        _dbContext.Accounts.Remove(user);
        _dbContext.SaveChanges();
        return Ok("Account deleted");
    }
    #endregion
}
