using Microsoft.AspNetCore.Mvc;
using TodoList.Models;

namespace TodoList.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoListController : ControllerBase
{
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
}
