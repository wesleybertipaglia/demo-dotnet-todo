# .NET TODO API

This project is a simple todo api built with dotnet core. It allows users to create, read, update and delete tasks.

## Features

The application includes the following functionalities:

- User registration and login
- Tasks creation, editing, and deletion
- Task status update

## ERD Diagram

The following Entity-Relationship Diagram (ERD) shows the relationships between the entities in the application:

```mermaid
classDiagram
    class User {
        uuid id
        string name
        string email
        string password
        datetime created_at
        datetime updated_at
    }
    
    class Todo {
        uuid id
        uuid userId
        string title
        string content
        string status
        datetime created_at
        datetime updated_at
    }

    User "1" *-- "N" Task : creates
```

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.
