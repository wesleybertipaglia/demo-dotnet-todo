namespace todo.DTOs;

public record TodoRequestDTO(string title, string description, string status, Guid userId);
