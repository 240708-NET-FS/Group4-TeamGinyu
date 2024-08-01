using System.ComponentModel.DataAnnotations.Schema;

namespace HelpMeCook.API.Models;

public class Recipe 
{
    public int RecipeID { get; set;}
    public string? RecipeName { get; set; }
    public DateTime CratedDate { get; set; }
    public int RecipeNumber { get; set; }
    public int UserID { get; set; }

    [ForeignKey("UserID")]
    public User? User { get; set; }
}