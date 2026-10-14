# TaskMaster

TaskMaster is a .NET console application for a training workflow. Designers create and maintain questions, interns submit answers, and designers review submissions to award points and progress intern levels.

## Features

- User registration, login, profile editing, and account deletion
- Designer and intern menus
- Question creation, editing, and deletion
- Questions filtered by intern level; a user cannot submit another answer to a question they have already answered
- Answer submission, status tracking, and designer review
- Approving an answer updates its status, grade, intern score, and level in one SQL transaction

## Requirements

- .NET 10 SDK
- SQL Server (local or remote)
- A SQL Server database and an account with permission to create and update tables

The application creates its `Users`, `Questions`, and `Answers` tables on first startup. Create the database itself before running the application:

```sql
CREATE DATABASE TaskMaster;
```

Configure the connection string in `TaskMaster/appsettings.json`. This file is ignored by Git because it may contain credentials. The application copies it to the output directory when building or publishing. Example:

```json
{
  "ConnectionStrings": {
    "TaskMaster": "Server=localhost;Database=TaskMaster;User ID=YOUR_USERNAME;Password=YOUR_PASSWORD;Encrypt=True;TrustServerCertificate=True"
  }
}
```

Replace `YOUR_USERNAME` and `YOUR_PASSWORD` with your SQL Server credentials. Change `Server=localhost` to the SQL Server host and instance you actually installed (for example, `localhost\SQLEXPRESS` for a default SQL Express instance). `TrustServerCertificate=True` is useful for a local development instance with a self-signed certificate. For production, configure a trusted SQL Server certificate and omit that option.

Run the application from the project directory:

```powershell
dotnet build .\TaskMaster.csproj
dotnet run --project .\TaskMaster.csproj
```

Or from the parent directory:

```powershell
dotnet build .\TaskMaster\TaskMaster.csproj
dotnet run --project .\TaskMaster\TaskMaster.csproj
```

## Clean Architecture layout

The console app remains a single .NET project, organized into Clean Architecture layers:

- `Domain/Entities` contains the user, question, and answer models without database or console dependencies.
- `Application/Abstractions/Persistence` defines the persistence ports used by the application.
- `Application/Services` contains registration, authentication, profile, question, and answer use cases. It depends on the domain and interfaces, not SQL Server.
- `Infrastructure/Persistence/SqlServer` implements the repository ports and database initialization with SQL Server.
- `Presentation/Cli/Menus` contains the console UI and display formatters; `Presentation/Cli/Validation` contains input validation.
- `Presentation/Program.cs` is the composition root: it wires SQL Server implementations into application services and starts the menus.

Dependency direction is inward: Infrastructure implements Application abstractions, and Presentation is the only layer that references the SQL Server implementation directly (for startup composition). The SQL statements are parameterized, update-column names are allowlisted, and answer review plus score progression is atomic.

The application creates or updates the `Users`, `Questions`, and `Answers` tables on startup. Existing `Answers` tables are migrated by dropping the obsolete `AnswerId` column; answer records and their identity keys are retained. Back up an existing SQL database before using the application. JSON files from older builds are not automatically imported; preserve and migrate any data you need before switching the application to SQL Server.

```text
TaskMaster/
├── Domain/
│   └── Entities/
├── Application/
│   ├── Abstractions/Persistence/
│   └── Services/
├── Infrastructure/
│   └── Persistence/SqlServer/
├── Presentation/
│   ├── Cli/Menus/
│   ├── Cli/Validation/
│   └── Program.cs
└── TaskMaster.csproj
```
