
namespace HelpMeCook.API.Models;

public class User 
{
    public int UserID { get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CratedDate { get; set; }

    // One-to-Many Relationship between User and Recipes
    // Initializing the list so that we can store returned Recipes later
    public ICollection<Recipe>? Recipes  { get; set; } = [];
    // One-to-One Relationship between User and Login
    // Initializing the list so that we can store returned Login later
    public Login? Login { get; set; } = new Login();
}