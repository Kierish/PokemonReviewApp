# PokemonReviewApp

A RESTful Web API built with **ASP.NET Core** and **Entity Framework Core**.  
This project is designed to manage Pokemon data, including reviews, owners, and categories, utilizing a relational database structure.

> **Note:** This project was developed independently as a detailed learning exercise suggested by an AI assistant. It is **not** a copy-paste project from a tutorial; the architecture and logic were implemented to understand the core concepts of backend development and the Repository Pattern.

## 🛠️ Tech Stack

*   **Framework:** .NET 6 / 7 (ASP.NET Core Web API)
*   **Language:** C#
*   **Database:** SQL Server
*   **ORM:** Entity Framework Core
*   **Documentation:** Swagger UI
*   **Architecture:** Repository Pattern, Dependency Injection

## ✨ Key Features

*   **CRUD Operations:** Full Create, Read, Update, Delete functionality for Pokemons.
*   **Advanced Relationships:** Handles Many-to-Many relationships (e.g., via `PokemonCategory` join table).
*   **Repository Pattern:** Decouples business logic from data access using `IPokemonRepository` and generic `IRepository` interfaces.
*   **Asynchronous Programming:** Fully async implementation (`Task`, `await`) for performance and scalability.
*   **Data Validation:** Checks for existing entities and valid states before committing to the database.

## 📂 Project Structure

*   **Controllers:** Handles HTTP requests (GET, POST, PUT, DELETE).
*   **Data:** Contains `DataContext` (EF Core context).
*   **Interfaces:** Defines contracts for Repositories (`IPokemonRepository`).
*   **Models:** Database entities (`Pokemon`, `Owner`, `Category`).
*   **Repositories:** Implementation of data access logic.

## 🚀 Getting Started

Follow these steps to set up the project locally.

### Prerequisites

*   **.NET SDK**
*   **SQL Server** (LocalDB or full instance)
*   **Visual Studio** or **VS Code**

### Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/Kierish/PokemonReviewApp.git
    cd PokemonReviewApp
    ```

2.  **Configure Database:**
    Update the connection string in `appsettings.json` to point to your local SQL Server instance.

3.  **Apply Migrations:**
    Open the terminal (Package Manager Console) and run:
    ```bash
    dotnet ef database update
    ```

4.  **Run the Application:**
    ```bash
    dotnet run
    ```
    Or press **F5** in Visual Studio.

5.  **Test the API:**
    Navigate to `https://localhost:7xxx/swagger` to inspect and test the endpoints.

## 🤝 Contributing

This is a learning project, but suggestions are welcome!

## 📄 License

This project is open-source.

---
**Author:** [Kierish](https://github.com/Kierish)
