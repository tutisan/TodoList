
using TodoList.Data;

namespace TodoList;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddDbContext<TodoListDbContext>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
