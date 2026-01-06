
namespace TodoList;

public class Program
{
    public static void Main(string[] args)
    {
        var app = WebApplication.Create(args);

        app.MapGet("/", () => "Hello, world!");

        app.Run();
    }
}
