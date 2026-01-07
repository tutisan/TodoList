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

    #region Endpoints
    [HttpPost]
    public IActionResult CreateTaskItem(TaskItemCreateDTO createNewTaskItem)
    {
        var newTaskItem = new TaskItem(createNewTaskItem.Name, createNewTaskItem.IsDone);
        _dbContext.TaskItems.Add(newTaskItem);
        _dbContext.SaveChanges();
        return Created("Task created", createNewTaskItem);
    }

    [HttpGet]
    public IActionResult GetAllTaskItems()
    {
        var items = _dbContext.TaskItems;
        return Ok(items.ToList());
    }

    [HttpGet("done")]
    public IActionResult GetDoneTaskItems()
    {
        var items = _dbContext.TaskItems.Where(item => item.IsDone == true);
        return Ok(items.ToList());
    }

    [HttpGet("{taskId}")]
    public IActionResult GetSingleTaskItem(Guid taskId)
    {
        var item = _dbContext.TaskItems.Find(taskId);

        if (item != null)
        {
            return Ok(new TaskItemDetailDTO(item.Name, item.IsDone));
        }

        return NotFound();
    }

    [HttpPut("{taskId}")]
    public IActionResult UpdateTaskItem(Guid taskId, TaskItemChangeDTO taskItemChange)
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
}
