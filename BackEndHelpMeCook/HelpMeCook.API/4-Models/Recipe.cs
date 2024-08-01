using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HelpMeCook.API.Models;

public class Recipe 
{
    public int RecipeID { get; set;}
    public string RecipeName { get; set; }
    public DateTime CratedDate { get; set; }
    public int RecipeNumber { get; set; }

    public User UserID  { get; set; }
}