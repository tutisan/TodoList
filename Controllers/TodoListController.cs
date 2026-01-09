using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using TodoList.DTOs;
using TodoList.Models;

namespace TodoList.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
    #region Controller setup
    private readonly TodoListDbContext _dbContext;

    public TodoListController()
    {
        _dbContext = new TodoListDbContext();
    }
    #endregion

    #region Debug endpoints
    [HttpGet("debug/list")]
    public IActionResult ListAllTasks()
    {
        var allTasks = _dbContext.TaskItems.ToList();
        return Ok(allTasks);
    }
    #endregion

    #region Endpoints
    [Authorize]
    [HttpPost]
    public IActionResult CreateTaskItem(CreateTaskDTO createNewTaskItem)
    {
        var loggedUser = GetAuthenticatedUser();

        if (loggedUser != null)
        {
            var newTaskItem = new TaskItem(createNewTaskItem.Name, createNewTaskItem.IsDone)
            {
                Author = loggedUser
            };
            _dbContext.TaskItems.Add(newTaskItem);
            _dbContext.SaveChanges();
            return Created("Task created", createNewTaskItem);
        }

        return Unauthorized();
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAllTaskItems()
    {
        var loggedUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (loggedUser != null)
        {
            var items = _dbContext.TaskItems.Where(t => t.Author.Username == loggedUser);
            return Ok(items.ToList());
        }

        return Unauthorized();
    }

    [Authorize]
    [HttpGet("done")]
    public IActionResult GetDoneTaskItems()
    {
        var loggedUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (loggedUser != null)
        {
            var items = _dbContext.TaskItems.Where(item => item.IsDone == true && item.Author.Username == loggedUser);
            return Ok(items.ToList());
        }

        return Unauthorized();
    }

    [Authorize]
    [HttpGet("{taskId}")]
    public IActionResult GetSingleTaskItem(Guid taskId)
    {
        var loggedUser = GetAuthenticatedUser();
        var item = _dbContext.TaskItems.Find(taskId);

        if (item != null && loggedUser != null && item.Author == loggedUser)
        {
            return Ok(new TaskDetailDTO(item.Name, item.IsDone));
        } 

        return NotFound();
    }

    [HttpPut("{taskId}")]
    public IActionResult UpdateTaskItem(Guid taskId, UpdateTask taskItemChange)
    {
        var item = _dbContext.TaskItems.Find(taskId);
        
        if (item != null)
        {
            if (taskItemChange.Name != null)
            {
                item.Name = taskItemChange.Name;
            }
            item.IsDone = taskItemChange.IsDone;
            _dbContext.SaveChanges();
            return Ok(item);
        }

        return NotFound();
    }

    [HttpDelete("{taskId}")]
    public IActionResult DeleteTaskItem(Guid taskId)
    {
        var item = _dbContext.TaskItems.Find(taskId);

        if (item != null)
        {
            _dbContext.TaskItems.Remove(item);
            _dbContext.SaveChanges();
            return NoContent();
        }

        return NotFound();
    }
    #endregion

    #region Util
    private Account? GetAuthenticatedUser()
    {
        var usernameInJWT = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return _dbContext.Accounts.FirstOrDefault(u => u.Username == usernameInJWT);
    }
    #endregion
}
