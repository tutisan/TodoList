
namespace TodoList.Models;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsDone { get; set; }
}
