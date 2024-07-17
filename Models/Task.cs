public class Task
{
    public Guid Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public User User { get; set; }

    public Task(string title, string description, bool isCompleted, User user)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
        User = user;
    }
}