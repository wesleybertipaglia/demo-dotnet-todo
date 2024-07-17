namespace todo.DTOs;

public record TodoResponseDTO(Guid id, string title, string description, string status, Guid userId);
