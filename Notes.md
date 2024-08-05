## This is how we setup the project so far.
- dotnet new webapi --name {myProjectName}.API -o ./{myProjectName}.API
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.EntityFrameworkCore.Tools

## Adding DBcontext for database
- builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("name of db")));

#### Add a new migration
- dotnet ef migrations add {NameofMigration}

#### Add a new .gitignore
- dotnet new gitignore

#### Update the database with the seeded data:
- dotnet ef migrations add SeedData
- dotnet ef database update

### Steps to run unittest report:
- dotnet test --collect:"XPlat Code Coverage" (This will generate a TestResult folder along with a guid.)
- reportgenerator -reports:".\TestResults\{guid}\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html -classfilters:"+SERVICES_NAMESPACE.*;"  (The -classfilters:"+SERVICES_NAMESPACE.*;" is optional, this is to generate a report only for the services.)