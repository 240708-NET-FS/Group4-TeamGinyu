
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HelpMeCook.API.Models;

public class User : IdentityUser
{
    
    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";
    
    public DateTime CratedDate { get; set; }

    // One-to-Many Relationship between User and Recipes
    // Initializing the list so that we can store returned Recipes later
    public ICollection<Recipe>? Recipes  { get; set; }
   
}