# Insurance Claims Management System

## Project Overview

A simple ASP.NET Core MVC application for managing insurance claims. The application allows users to create, view, edit, delete, and track basic insurance claims using Entity Framework Core and a SQLite database.

## Features

- Claim Management: Full CRUD operations for insurance claims
- Search and Filtering: Search by customer name or policy number, filter by claim status or claim type
- Dashboard: Overview with total claims and status distribution
- Validation: Client-side and server-side validation with clear error messages
- Error Handling: Friendly error pages and comprehensive logging
- Unit Testing: Tests covering service logic and validation
- CI/CD: GitHub Actions for automated build and test

## Technologies Used

- ASP.NET Core MVC 8.0
- Entity Framework Core 8.0
- SQLite Database
- Bootstrap 5
- xUnit for testing
- Moq for mocking
- GitHub Actions for CI/CD

## Setup Instructions

1. Clone the repository:
   git clone https://github.com/raafaomena/BSC20925_76122_Resit.git
   cd BSC20925_76122_Resit

2. Restore dependencies:
   dotnet restore

3. Build the project:
   dotnet build

4. Apply database migrations:
   cd BSC20925_76122_Resit.Web
   dotnet ef database update
   cd ..

5. Run the application:
   dotnet run --project BSC20925_76122_Resit.Web

6. Open your browser and navigate to:
   http://localhost:5296

## Database Setup

The application uses SQLite with Entity Framework Core.

### Manual Migration Commands

cd BSC20925_76122_Resit.Web
dotnet ef migrations add InitialCreate
dotnet ef database update

### Seed Data

The database is pre-seeded with 4 sample claims.

## Testing

Run the test suite:

dotnet test

## Author

Student Number: 76122
Module: BSC20925 - Modern Programming Principles and Practice 1
