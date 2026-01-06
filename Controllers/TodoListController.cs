using Microsoft.AspNetCore.Mvc;

namespace TodoList.Controllers;

[ApiController]
[Route("/")]
public class TodoListController : ControllerBase
{
    [HttpGet]
    public string Home() => "Hello, world!";
}
