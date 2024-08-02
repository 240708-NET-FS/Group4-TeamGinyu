using Microsoft.EntityFrameworkCore;
using HelpMeCook.API.Models;

namespace HelpMeCook.API.DAO;

public class AppDbContext : DbContext {

    public AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options ){ }

    public DbSet<Login> Login { get; set;}
    public DbSet<User> User { get; set; }
    public DbSet<Recipe> Recipe { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<User>().HasData(
        //     new User { UserID = 1, FirstName = "John", LastName = "DOE", CreatedDate = DateTime.Now },
        //     new User { UserID = 2, FirstName = "Albert", LastName = "FAT", CreatedDate = DateTime.Now }
        // );

        // modelBuilder.Entity<Login>().HasData(
        //     new Login { LoginID = 1, Username = "jhon.doe", Password = "pass1", UserID = 1 },
        //     new Login { LoginID = 2, Username = "albert.fat", Password = "pass2", UserID = 2 }
        // );
    }
}