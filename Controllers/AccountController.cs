using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using TodoList.Data;
using TodoList.DTOs;
using TodoList.Models;

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
