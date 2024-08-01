
namespace HelpMeCook.API.Models;

public class LoginDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public int UserID  { get; set; }
}
