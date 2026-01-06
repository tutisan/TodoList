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
        _dbContext.TaskItems.Add(taskItem);
        _dbContext.SaveChanges();
        return Created("Task created", taskItem);
    }

    [HttpGet]
    public IActionResult GetAllTaskItems()
    {
        var items = _dbContext.TaskItems;
        return Ok(items.ToList());
    }

    [HttpGet("{taskId}")]
    public IActionResult GetTaskItemById(Guid taskId)
    {
        var item = _dbContext.TaskItems.Find(taskId);

        if (item != null)
        {
            return Ok(item);
        }

        return NotFound();
    }

    [HttpPut("{taskId}")]
    public IActionResult UpdateTaskItem(Guid taskId, TaskItem taskItem)
    {
        var item = _dbContext.TaskItems.Find(taskId);
        
        if (item != null)
        {
            item.Name = taskItem.Name;
            item.IsDone = taskItem.IsDone;
            _dbContext.SaveChanges();
            return Ok(item);
        }

        return NotFound();
    }

    [HttpDelete("{taskId}")]
    public IActionResult DeleteTaskItem(Guid taskId)
    {
        throw new NotImplementedException();
    }
    #endregion
}
