
namespace TodoList.DTOs;

public class TaskItemDetailDTO
{
    public string Name { get; set; }
    public bool IsDone { get; set; }

    public TaskItemDetailDTO(string name, bool isDone)
    {
        Name = name;
        IsDone = isDone;
    }
}
