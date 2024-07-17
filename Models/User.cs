namespace todo.Models;

#pragma warning disable CS8618

public class User
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User()
    {
    }

    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
    }
}