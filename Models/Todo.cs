namespace todo.Models;

public class Todo
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public User User { get; set; }

    public Todo(string title, string description, string status, User user)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Status = status;
        User = user;
    }
}