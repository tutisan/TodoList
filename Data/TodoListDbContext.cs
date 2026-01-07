using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Data;

public class TodoListDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }

    public string DbPath { get; }
    
    public TodoListDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "todolist.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }
}
