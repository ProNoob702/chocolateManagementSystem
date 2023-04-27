# Intro 
This project is Chocolate factory and wholesale management system sample built on Clean Architecture.
Database is filled with data after application start.

# Design Patterns
- CQRS design pattern
- Mediator design pattern
- Repository design pattern
- Dependency Injection pattern

# Environment 
- .NET 7
- Entityframework Core 7
- Automapper
- FluentValidation
- XUnit
- Swagger

# Solution projects 
- ChocolateManagementSystem.Domain : Contains domain models
- ChocolateManagementSystem.Application: Contains Interfaces and Commands/Queries along with handlers and validators
- ChocolateManagementSystem.Infrastructure : Contains database related logic with data seeding
- ChocolateManagementSystem.API: Contains API Controllers
- ChocolateManagementSystem.Application.Tests: Contains Unit tests for 2 commands 

# Screenshots 

1- FR1- List all the chocolate bar by chocolate factory:
![image](https://user-images.githubusercontent.com/16271638/234970540-61c81d25-a77c-4a0e-954d-5f2375ea8dce.png)

2- FR6- Request a quote from a wholesaler:

[Failure] when data passed isn't valid
![image](https://user-images.githubusercontent.com/16271638/234971036-2a627ed3-3803-431b-b55a-165b266bf69a.png)

[Success] when data passed is valid
![image](https://user-images.githubusercontent.com/16271638/234971556-d21360e6-2c19-4e60-bd09-2281a477fb0a.png)

3- Unit tests [4 Passed]
![image](https://user-images.githubusercontent.com/16271638/234971773-519032d4-2162-4029-a7bf-cab022d2baa3.png)

