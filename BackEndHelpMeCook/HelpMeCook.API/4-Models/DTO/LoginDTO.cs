
namespace HelpMeCook.API.Models.DTO;

public class LoginDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginWithUserIDDTO : LoginDTO
{
   public int UserID  { get; set; }
}