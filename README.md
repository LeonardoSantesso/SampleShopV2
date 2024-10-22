## Project: SampleShopV2 - .NET 8 Web API with Azure Functions and SQL Server

### Overview:
This project is a refactored version of a technical test, originally developed using .NET Core 3.1 and Azure Functions. To showcase my technical expertise and experience, I updated the project to .NET 8, improved the architecture following Domain-Driven Design (DDD), integrated SQL Server using Entity Framework, and created a robust Web API. The project demonstrates the key aspects of modern web development, including authentication, data mapping, and unit testing.

### Technologies Used:
- **.NET 8.0**
- **Azure Functions V4 In-Process Model**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **Bearer Token Authentication**
- **Domain-Driven Design (DDD)**
- **Unit Testing (XUnit)**
- **Entity Framework In-Memory for Testing**

### Key Features:

#### 1. Azure Functions:
- **Migration from .NET Core 3.1 to .NET 8.0**: Migrated the entire project from the older .NET Core version to .NET 8, utilizing the latest Azure Functions V4 In-Process model for optimal performance.
- **Domain-Driven Design (DDD) Architecture**: The project is structured around DDD principles, with clearly defined domains, services, and repositories. This ensures modularity, scalability, and maintainability.
- **Entity Framework Integration with SQL Server**: Fully integrated with SQL Server using Entity Framework Core, with models, migrations, and database context set up for data persistence.
- **Repository Pattern**: Implemented the repository pattern to encapsulate data access logic and ensure clean separation of concerns between business logic and data access layers.
- **Unit Testing with In-Memory Database**: Created comprehensive unit tests for domain and service layers using Entity Framework’s In-Memory provider to simulate real data and scenarios for testing.
- **Data Builder for Testing**: A custom data builder is used to generate test data, allowing for easy setup of unit tests with valid data.

#### 2. Web API Development:
- **.NET 8 Web API**: Developed a robust API to expose all core functionalities, including CRUD operations on Orders and querying orders by date ranges.
- **Order Management Endpoints**:
  - **GET /api/orders**: Retrieve all orders.
  - **GET /api/orders/{id}**: Retrieve a specific order by its ID.
  - **GET /api/orders/dates?start={start}&end={end}**: Retrieve orders between specified date ranges.
  - **GET /api/orders/items?day={YYYY-MM-DD}**: Retrieve items sold on a specific day.
  - **POST /api/orders**: Create a new order.
  - **DELETE /api/orders/{id}**: Delete an order by ID.
- **Authentication with Bearer Token**: Implemented authentication using Bearer Token to secure endpoints, ensuring only authorized users can access certain API functionalities.
- **AutoMapper Configuration**: Set up AutoMapper to simplify the mapping between domain entities and Data Transfer Objects (DTOs).

#### 3. Unit Testing:
- **In-Memory Database for Tests**: Used Entity Framework In-Memory to simulate real data and perform realistic testing without relying on external databases.
- **Extensive Unit Tests**: Tested core domain logic and services with detailed assertions to ensure the project is robust and error-free.
- **Data Builders**: Created builders to streamline test case setup with valid data for consistent and reliable tests.

### Project Structure:
- **Domain Layer**: Contains core business logic and domain entities like `Order` and `OrderItem`, following Domain-Driven Design principles.
- **Infrastructure Layer**: Handles data persistence with repositories, Entity Framework, and database-related operations.
- **Application Layer**: Hosts services that encapsulate the domain logic and provide an abstraction layer for the API and Azure Functions.
- **API Layer**: The Web API built with .NET 8, which exposes endpoints for external interaction with the core business logic.
- **Azure Functions**: Set of Azure Functions for serverless operations, supporting both the new API and additional functionality.

### Improvements in the Migration:
- **Performance Upgrades**: Leveraging .NET 8 and the latest Azure Functions model for improved speed and efficiency.
- **Scalability**: The DDD architecture and separation of concerns make the project scalable and easier to maintain.
- **Security**: Implemented token-based authentication using Bearer Token, ensuring secure access to API endpoints.
- **Extensibility**: With repositories and services structured in a modular way, adding new features or extending functionality is straightforward.

### How to Run the Project:
1. Clone the repository.
2. Set up a SQL Server instance and update the connection string in `appsettings.json`.
3. Run the migrations to set up the database: `dotnet ef database update`.
4. Start the project: `dotnet run`.
5. Use an API testing tool (e.g., Postman) to interact with the available endpoints.
