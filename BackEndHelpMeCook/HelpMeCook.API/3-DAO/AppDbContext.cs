using Microsoft.EntityFrameworkCore;
using HelpMeCook.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HelpMeCook.API.DAO;

public class AppDbContext : IdentityDbContext<User> {

    public AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options ){ }

    public DbSet<Recipe> Recipe { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserID);
    }
}