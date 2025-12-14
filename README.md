# Loan Management System

A full-stack loan management application built with .NET Core and Angular.

## Technologies Used
- .NET 8.0
- Entity Framework Core
- SQL Server
- Angular 17
- Docker

## Prerequisites
- .NET 8 SDK
- Node.js 18+
- Docker Desktop
- SQL Server (or Docker)

## Setup Instructions

### Using Docker (Recommended)
1. Clone the repository
2. Run: `docker-compose up --build`
3. API: http://localhost:5000
4. Swagger: http://localhost:5000/swagger

### Manual Setup

#### Backend
```bash
cd backend/LoanManagement.API
dotnet restore
dotnet ef database update
dotnet run
```

#### Frontend
```bash
cd frontend/loan-app
npm install
ng serve
```

## API Endpoints
- POST /api/loans - Create loan
- GET /api/loans - Get all loans
- GET /api/loans/{id} - Get loan by ID
- POST /api/loans/{id}/payment - Make payment

## Testing
```bash
cd backend
dotnet test
```

## Implementation Notes
- Clean architecture with repository pattern
- EF Core migrations with seed data
- CORS enabled for Angular frontend
- Unit tests with xUnit
- Docker containerization