# GenesisNightclub

## How to use with Sql Server / In Memory

### In Memory Setup

To use this API with in memory go to `GenesisNightclub.Web/Program.cs`  and toggle comment for these lines :
```
builder.Services.AddSingleton<IMemberRepository, InMemoryMemberRepository>();
//builder.Services.AddScoped<IMemberRepository, SqlMemberRepository>();
```

### SQL Server Setup

If you want to use Sql Server, don't change Program.cs just edit `appsettings.Development.json` where the key is `MSSQLServerConnection`:
```
{
  ...
  "ConnectionStrings": {
    "MSSQLServerConnection": "Server=localhost;Database=genesis_consult;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

If you setup this line correctly, the tables in the database will be created automatically.

## How to test

Swagger is used and opens automatically when the program is started. All API endpoints will be shown with examples and it will be possible to try them out.
