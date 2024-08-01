
namespace HelpMeCook.API.Models.DTO;

public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CratedDate { get; set; }
}

public class RecipeWithUserIDDTO : UserDTO
{
   public int UserID  { get; set; }
}

