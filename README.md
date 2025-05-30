# APBD_6 – Client and Trip Management API (Database First approach)

This project is a RESTful API for managing clients and trips. It supports operations such as retrieving trip details, assigning clients to trips, and removing clients under certain conditions.

## Project Setup Instructions

### Database Preparation (SQL Server)

1. **Drop the database if it exists**
   Run:

   ```sql
   -- cw12_drop.sql
   -- alters tables and drops tables
   ```

2. **Create the database schema**
   Run:

   ```sql
   -- cw12_create.sql
   -- create tables and alter tables
   ```

3. **Insert test data**
   Run:

   ```sql
   -- insert.sql
   -- insert sample data into tables
   ```

   **Alternative way**: use `Seeder.cs` to insert seed data at runtime.

---

### Regenerating Models with EF Core Scaffold

> Do this if you're starting from scratch with a clean DB or simply want to verify the Project

1. **Temporarily move** all `.cs` files from these folders:

    * `Controllers/`
    * `Data/`
    * `Repositories/`
    * `Services/`

2. **Comment out dependencies** in `Program.cs`:

   ```csharp
   // using APBD_6.Data;
   // using APBD_6.Repositories;
   // using APBD_6.Services;
   // builder.Services.AddDbContext<AppDbContext>(...);
   // builder.Services.AddScoped<ITripRepository, TripRepository>();
   // builder.Services.AddScoped<ITripService, TripService>();
   // builder.Services.AddScoped<IClientRepository, ClientRepository>();
   // builder.Services.AddScoped<IClientService, ClientService>();
   ```

3. **Scaffold the database**:

   ```bash
   dotnet ef dbcontext scaffold "Server=localhost,1433;User=SA;Password=yourStrong(!)Password;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext --context-dir Data --use-database-names
   ```
   
   **If you use a different connection string, just change it here**

4. After Scaffold **Clean up `AppDbContext.cs`**:

    * Comment out or remove the default constructor:

      ```csharp
      // public AppDbContext() { }
      ```
    * Comment our or remove `OnConfiguring`:

      ```csharp
      // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      ```

5. **Restore your code**
   Move the `.cs` files back to their original folders.

6. **Uncomment** the previously disabled code in `Program.cs`.

7. **Run the project**:

   ```bash
   dotnet run
   ```

---

### API Testing

Use `APBD_6.http` or tools like Postman/Rider to verify:

#### `GET /api/trips`

```json
{
  "pageNum": 1,
  "pageSize": 5,
  "allPages": 1,
  "trips": [
    {
      "name": "ABC",
      "description": "...",
      "countries": [ { "name": "Poland" }, { "name": "Germany" } ],
      "clients": [ { "firstName": "John", "lastName": "Smith" } ]
    },
    ...
  ]
}
```

#### `DELETE /api/clients/3`

Removes unassigned client:

```http
HTTP/1.1 204 No Content
```

#### `DELETE /api/clients/1`

Fails if a client is assigned to a trip:

```http
HTTP/1.1 400 Bad Request
"Client cannot be deleted: either does not exist or has trips assigned."
```

#### `POST /api/trips/3/clients`

Assigns new client:

```http
HTTP/1.1 200 OK
"Client assigned successfully."
```

---

### Configuration

Your connection string is defined in:

* `appsettings.json`
* `appsettings.Development.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=localhost,1433;User=SA;Password=yourStrong(!)Password;..."
}
```

Update this string to match your SQL Server setup if needed.

---

### Previous Projects
**[APBD_5 - EF (Code First) App that helps to manage prescriptions](https://github.com/x0-lf/APBD_5)**

**[APBD_4 - Warehouse Management](https://github.com/x0-lf/APBD_4)**

**[APBD_3 - REST API Travel Agency](https://github.com/x0-lf/APBD_3)**

**[APBD_2 - Simple 20 LINQ xUnit Tests](https://github.com/x0-lf/APBD_2)**

**[APBD_1 - Simple yet Advanced Logistics Manager Example](https://github.com/x0-lf/APBD_1)**

