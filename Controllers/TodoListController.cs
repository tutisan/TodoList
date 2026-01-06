using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
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

    #region Endpoints
    [HttpPost]
    public IActionResult CreateTaskItem(TaskItem taskItem)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public IActionResult GetAllTaskItems()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{taskId}")]
    public IActionResult GetTaskItemById(Guid taskId)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{taskId}")]
    public IActionResult UpdateTaskItem(Guid taskId, TaskItem taskItem)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{taskId}")]
    public IActionResult DeleteTaskItem(Guid taskId)
    {
        throw new NotImplementedException();
    }
    #endregion
}
