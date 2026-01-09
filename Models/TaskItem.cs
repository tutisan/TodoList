
namespace TodoList.Models;

public class TaskItem(string name, bool isDone)
{
    public Guid Id { get; set; }
    public string Name { get; set; } = name;
    public bool IsDone { get; set; } = isDone;

    public Guid AuthorId { get; set; }
    public Account Author { get; set; }
}
