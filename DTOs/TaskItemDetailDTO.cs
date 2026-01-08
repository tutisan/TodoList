
namespace TodoList.DTOs;

public class TaskItemDetailDTO(string name, bool isDone)
{
    public string Name { get; set; } = name;
    public bool IsDone { get; set; } = isDone;
}
