# Insurance Claims Management System

## Project Overview
A simple ASP.NET Core MVC application for managing insurance claims.

## Features
- CRUD Operations: Create, Read, Update, Delete insurance claims
- Search and Filter: Search by customer name or policy number
- Filter by Status: Filter claims by their current status
- Filter by Type: Filter claims by claim type
- Validation: Client-side and server-side validation
- Error Handling: Friendly error pages and exception handling
- Dashboard: Overview of claims by status with key metrics
- Unit Tests: xUnit tests for models and services
- CI/CD: GitHub Actions for automated build and test

## Technologies Used
- ASP.NET Core MVC (.NET 8.0)
- Entity Framework Core 8.0 with SQLite
- Bootstrap 5 for responsive UI
- xUnit and Moq for testing
- GitHub Actions for CI/CD

## Student Information
- Student Number: 76122
- Module: BSC20925 - Modern Programming Principles and Practice 1
- Assessment: Resit Assignment 2026

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK
- SQLite (included)

### Installation

1. Clone the repository:
git clone https://github.com/raafaomena/BSC20925_76122_Resit.git
cd BSC20925_76122_Resit

2. Restore dependencies:
dotnet restore

3. Build the project:
dotnet build

4. Apply database migrations:
dotnet ef database update --project BSC20925_76122_Resit.Web

5. Run the application:
dotnet run --project BSC20925_76122_Resit.Web

6. Navigate to: https://localhost:5001 or http://localhost:5000

## Database Setup
The application uses SQLite with Entity Framework Core. Migrations are applied automatically on startup. Seed data includes 3 sample claims for testing.

## Testing
Run the test suite:
dotnet test

## Project Structure
BSC20925_76122_Resit/
├── .github/workflows/     # CI/CD pipelines
├── BSC20925_76122_Resit.Web/
│   ├── Controllers/       # MVC Controllers
│   ├── Models/            # Domain models and enums
│   ├── Views/             # Razor views
│   ├── Data/              # DbContext and migrations
│   ├── Services/          # Business logic layer
│   └── Program.cs
├── BSC20925_76122_Resit.Tests/  # Unit tests
└── README.md

## CI/CD
This project uses GitHub Actions for continuous integration:
- Builds on push to main and develop branches
- Runs all unit tests
- Publishes build artifacts

## Known Issues
- No authentication/authorization implemented
- Pagination not implemented on the claims list

## License
This project is for educational purposes as part of the BSC20925 module.
