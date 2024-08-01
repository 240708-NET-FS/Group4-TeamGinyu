using System.ComponentModel.DataAnnotations.Schema;

namespace HelpMeCook.API.Models;

public class Login 
{
    public int LoginID{ get; set;}
    public string Username { get; set; } = "";
    public  string Password { get; set; } = "";
    public int UserID { get; set; }

    [ForeignKey("UserID")]
    public User? User  { get; set; }

}