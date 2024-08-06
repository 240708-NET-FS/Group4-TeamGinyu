namespace HelpMeCook.API.Models;

public class RecipeDTO
{
    public string RecipeName { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public int RecipeNumber { get; set; }
    public string UserID { get; set; }
}