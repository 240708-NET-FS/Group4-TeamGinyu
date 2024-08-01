
namespace HelpMeCook.API.Models;

public class Login 
{
    public int LoginID{ get; set;}
    public string Username { get; set; }
    public string Password { get; set; }

    public User UserID  { get; set; }

}