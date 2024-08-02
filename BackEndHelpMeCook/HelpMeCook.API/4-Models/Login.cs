using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpMeCook.API.Models;

[Index(nameof(Username), IsUnique = true)]
public class Login 
{
    public int LoginID{ get; set;}
    
    [Required, System.ComponentModel.Description("The username is required and unique.")]
    public string Username { get; set; } = "";
    public  string Password { get; set; } = "";
    public int UserID { get; set; }

    [ForeignKey("UserID")]
    public User? User  { get; set; }

}