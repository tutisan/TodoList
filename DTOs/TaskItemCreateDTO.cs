
namespace TodoList.DTOs;

public class TaskItemCreateDTO
{
    public string Name { get; set; }
    public bool IsDone { get; set; }

    public TaskItemCreateDTO(string name, bool isDone)
    {
        Name = name;
        IsDone = isDone;
    }

    public TaskItemCreateDTO(string name)
    {
        Name = name;
    }
}
