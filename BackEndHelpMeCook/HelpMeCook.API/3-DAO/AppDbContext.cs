using Microsoft.EntityFrameworkCore;
using HelpMeCook.API.Models;

namespace HelpMeCook.API.DAO;

public class AppDbContext : DbContext {

    public AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options ){ }

    public DbSet<Login> Login { get; set;}
    public DbSet<User> User { get; set; }
    public DbSet<Recipe> Recipe { get; set; }

}