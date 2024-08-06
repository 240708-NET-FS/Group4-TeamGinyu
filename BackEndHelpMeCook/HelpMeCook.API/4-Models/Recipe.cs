using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpMeCook.API.Models;

public class Recipe 
{
    [Key]
    public int RecipeID { get; set;}
    public string? RecipeName { get; set; }
    public DateTime CreatedDate { get; set; }
    public int RecipeNumber { get; set; }
    public string UserID { get; set; }

    [ForeignKey("UserID")]
    public User? User { get; set; }
}