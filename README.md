# Movie API - A Sample Project for Learning Clean Architecture and Web APIs

## Introduction

This is a sample test project designed to **learn and teach Clean Architecture** principles, demonstrating how to use **MediatR** and other related dependencies for building scalable, maintainable web APIs and microservices.

The project is structured to showcase how to create **web APIs** following Clean Architecture, leveraging **MediatR** for handling commands, queries, and responses. It is a demonstration of **separation of concerns**, with **commands**, **queries**, and **handlers** isolated for easier maintainability and testing.

This repository provides an easy-to-follow solution structure, ideal for learning purposes. It also serves as a base template for building real-world web APIs and microservices using modern tools and practices.

---

## Key Features

- **Clean Architecture** principles to maintain modularity, scalability, and maintainability.
- **MediatR** for handling commands, queries, and responses, allowing for simple and flexible communication between components.
- **Dependency Injection** setup in ASP.NET Core for better testability and maintainability.
- **Entity Framework Core** for database interaction.
- **Repository Pattern** to abstract database access and support various operations on the database.
- Clear separation of **application logic**, **database access**, and **API controllers** for better manageability.

---

## Technologies Used

- **ASP.NET Core** for building the web API.
- **MediatR** for implementing the CQRS (Command Query Responsibility Segregation) pattern.
- **Entity Framework Core** for data access.
- **AutoMapper** for object-to-object mapping between entities and DTOs (Data Transfer Objects).
- **Dependency Injection** to decouple components and make them easier to test.
- **SQL Server** (or any other database) for storing data.

---

## How It Works

### MediatR in Action

The core of the application is built around **MediatR**. The project demonstrates how MediatR simplifies the interaction between controllers, commands, queries, and handlers. It uses **CQRS (Command Query Responsibility Segregation)** to handle operations like create, update, delete, and retrieve data separately through commands and queries.

1. **Command**: A class representing an action (e.g., creating or deleting a movie).
2. **Query**: A class representing a request to retrieve data (e.g., getting movies by director).
3. **Handler**: A class that handles the logic for the corresponding command or query.

For example, the `CreateMovieCommand` will be sent to a `CreateMovieCommandHandler` to process the request and return the appropriate response.

### Clean Architecture

The project follows **Clean Architecture** by keeping different concerns in separate layers:

- **Controllers**: Handle HTTP requests and responses.
- **Application Layer**: Handles the business logic, commands, queries, and MediatR handlers.
- **Domain Layer**: Contains core entities and models.
- **Infrastructure Layer**: Responsible for data access, repositories, and EF Core context.

---

## Learning Objectives

- Learn how to build scalable web APIs with **Clean Architecture** principles.
- Understand how to use **MediatR** for handling commands and queries in a decoupled way.
- Discover the **CQRS pattern** for separating read and write operations.
- Implement **dependency injection** in ASP.NET Core and properly manage dependencies.
- Work with **Entity Framework Core** for data access and implement repository patterns.

---

## Conclusion

This project serves as a starting point for anyone looking to learn Clean Architecture and modern techniques for building web APIs. It’s designed to be simple and easy to follow while also providing a solid foundation for creating scalable and maintainable applications.

Feel free to fork this repository, use it as a template, and start building your own APIs following these best practices!
