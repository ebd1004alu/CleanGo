# CleanGo Developer Guide

## Project Overview
CleanGo is a marketplace for cleaning services, connecting clients and professional cleaners. The project follows **Clean Architecture principles**.

## Project Structure
CleanGo/
├── src/
│ ├── CleanGo.API/ # Controllers, Program.cs, Startup
│ ├── CleanGo.Application/ # Use cases, services, DTOs
│ ├── CleanGo.Domain/ # Entities, interfaces, business logic
│ └── CleanGo.Infrastructure/ # Repositories, EF Core DbContext
├── tests/ # Unit and integration tests
├── docs/ # Documentation
└── CleanGo.sln # Solution file

## Requirements
- .NET 9 SDK
- PostgreSQL (running in Docker or locally)
- Docker and Docker Compose
- Visual Studio 2022 or compatible IDE
- Node.js (for frontend, if applicable)

## Running the API
### 1. Start PostgreSQL container:
```bash
docker compose -f docker-compose.postgres.yml up -d
```

### 2. Ensure connection string in appsettings.Development.json is configured:
```bash
"ConnectionStrings": {
  "CleanGoDb": "Host=localhost;Port=5432;Database=CleanGoDb;Username=cleango_user;Password=cleango_pass"
}
```

### 3. Run API from Visual Studio or via CLI:
```bash
dotnet run --project src/CleanGo.API
```
## Running Tests
```bash
dotnet test src/tests
```

## Database and EF Core
### Migrations are in CleanGo.Infrastructure/Migrations
#### To add a migration:
```bash
Add-Migration MigrationName -Project CleanGo.Infrastructure -StartupProject CleanGo.API
```
#### To apply migrations:
```bash
Update-Database -Project CleanGo.Infrastructure -StartupProject CleanGo.API
```

## Docker
#### PostgreSQL is run in a container for easy setup.
#### API currently runs locally connecting to Docker container.

## Git Workflow
#### Branch naming: feature/#-Description
#### Commits: short title + detailed description
#### Pull requests: link to issue using Closes #X

## GitHub Actions CI/CD
#### The project uses GitHub Actions for continuous integration. 
#### - Workflow file: `.github/workflows/ci.yml`
#### - Automatically builds the solution and runs tests on push and pull request to `main` and `develop`.
