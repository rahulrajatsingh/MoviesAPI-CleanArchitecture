# Movie API - A Sample Project for Learning Clean Architecture and Web APIs

## Introduction

This is a sample test project designed to **learn and teach Clean Architecture** principles, demonstrating how to build scalable, maintainable web APIs and microservices.

The project follows a **Service Layer Pattern**, where the **Service Layer** acts as an intermediary between the **API controllers** and **Repositories**. This approach keeps the business logic centralized and improves maintainability by providing a clear separation of concerns.

This repository provides an easy-to-follow solution structure, ideal for learning purposes. It also serves as a base template for building real-world web APIs and microservices using modern tools and practices.

---

## Key Features

- **Service Layer Pattern** to maintain modularity, scalability, and maintainability.
- **Dependency Injection** setup in ASP.NET Core for better testability and maintainability.
- **Entity Framework Core** for database interaction.
- **Repository Pattern** to abstract database access and support various operations on the database.
- **AutoMapper** for object-to-object mapping between entities and DTOs (Data Transfer Objects).
- Clear separation of **application logic**, **database access**, and **API controllers** for better manageability.

---

## Technologies Used

- **ASP.NET Core** for building the web API.
- **Entity Framework Core** for data access.
- **AutoMapper** for simplifying object mapping.
- **Dependency Injection** to decouple components and make them easier to test.
- **SQL Server** (or any other database) for storing data.

---

## How It Works

### Service Layer Pattern

The **Service Layer** sits between the **Controllers** and the **Repositories**. It encapsulates business logic and acts as an **intermediary** between the API layer and the data layer.

1. **Controller**: Handles HTTP requests and calls the corresponding service methods.
2. **Service Layer**: Contains business logic and interacts with repositories.
3. **Repository Layer**: Handles data access using Entity Framework Core.
4. **Database**: Stores the actual movie data.

### Solution Structure

```
📂 Movies.API               ---> (Controllers handle HTTP concerns)
   ├── MoviesController.cs  ---> Calls IMovieService

📂 Movies.Application       ---> (Application logic lives here)
   ├── Services
   │   ├── IMovieService.cs   ---> Defines service contract
   │   ├── MovieService.cs    ---> Implements business logic (calls repositories)
   ├── Models (Request/Response DTOs)
   ├── Mappings (AutoMapper Profiles)

📂 Movies.Core              ---> (Domain layer with business entities)
   ├── Entities
   │   ├── Movie.cs
   ├── Repositories
   │   ├── IMovieRepository.cs   ---> Defines repository contract
   │   ├── MovieRepository.cs    ---> Implements data access logic

📂 Movies.Infrastructure    ---> (Persistence layer)
   ├── Data
   ├── EF Core implementation of repositories
```

---

## Learning Objectives

- Learn how to build scalable web APIs with **Clean Architecture** principles.
- Understand how to use **Service Layer Pattern** for managing business logic.
- Implement **dependency injection** in ASP.NET Core and properly manage dependencies.
- Work with **Entity Framework Core** for data access and implement repository patterns.
- Utilize **AutoMapper** for seamless data transformations between entities and DTOs.

---

## Conclusion

This project serves as a starting point for anyone looking to learn Clean Architecture and modern techniques for building web APIs. It’s designed to be simple and easy to follow while also providing a solid foundation for creating scalable and maintainable applications.

Feel free to fork this repository, use it as a template, and start building your own APIs following these best practices!

