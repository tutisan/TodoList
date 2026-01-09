
namespace TodoList.DTOs;

public class CreateTaskDTO(string name, bool isDone)
{
    public string Name { get; set; } = name;
    public bool IsDone { get; set; } = isDone;
}
