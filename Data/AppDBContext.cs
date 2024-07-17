namespace todo.Data;

using Microsoft.EntityFrameworkCore;
using todo.Models;

public class AppDBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database.sqlite");
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        base.OnConfiguring(optionsBuilder);
    }
}
