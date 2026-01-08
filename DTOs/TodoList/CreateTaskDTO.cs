
namespace TodoList.DTOs;

public class CreateTaskDTO
{
    public string Name { get; set; }
    public bool IsDone { get; set; }

    public CreateTaskDTO(string name, bool isDone)
    {
        Name = name;
        IsDone = isDone;
    }

    public CreateTaskDTO(string name)
    {
        Name = name;
    }
}
