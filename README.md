# Todo List API

A .NET 8 Azure Functions API for managing todo items. Built using isolated worker process model with Entity Framework Core for data access.

## Usage  
 - Clone the Repository
 - Update local.settings.json with a connection string for "TodoConnectionString". To use a local database (for development etc) use:
   ```
    "ConnectionStrings": {
    "TodoConnectionString": "Server=(localdb)\\mssqllocaldb;Database=TodoDB;Trusted_Connection=True;MultipleActiveResultSets=true"
    } 
   ```
 - Apply database migrations by running the command "dotnet ef database update"
 - Run the project
The api will be hosted on your local machine, with documenation at <host>:<port>/api/swagger/ui.

## Endpoints
 - POST /api/todo - Create a todo item
 - GET /api/todo/{id} - Get a specific todo item
 - GET /api/todo - Get all todo items
 - GET /api/todo/query - Query todo items with filters
 - PUT /api/todo/{id} - Update a todo item
