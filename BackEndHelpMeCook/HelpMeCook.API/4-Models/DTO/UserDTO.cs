
namespace HelpMeCook.API.Models;

public class UserDTO
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CratedDate { get; set; }
}

public class RecipeWithUserIDDTO : UserDTO
{
   public int UserID  { get; set; }
}

