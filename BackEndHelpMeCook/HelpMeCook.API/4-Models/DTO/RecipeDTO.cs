
namespace HelpMeCook.API.Models;

public class RecipeDTO
{
    public string RecipeName { get; set; } = null!;
    public DateTime CratedDate { get; set; }
    public int RecipeNumber { get; set; }
    public int UserID { get; set; }
}