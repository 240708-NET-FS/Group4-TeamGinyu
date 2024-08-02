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