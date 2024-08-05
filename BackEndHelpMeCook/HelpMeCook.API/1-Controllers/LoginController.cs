using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace HelpMeCook.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
     private readonly ILoginService _loginService;

     public LoginController(ILoginService loginService)
     {
          this._loginService = loginService;
     }

     [HttpPost("login/signup")]
     public async Task<IActionResult> CreateLogin(LoginDTO loginDTO)
     {
          try
          {
               Login login = await _loginService.CreateLogin(loginDTO);
               return Ok(login);
               
          } catch (InvalidLoginException e)
          {
               return NotFound(e.Message);
          }
     }

     [HttpGet("login/{id}")]
     public async Task<IActionResult> GetLoginByID(int id)
     {
          var login = await _loginService.GetLoginByID(id);

          if (login is null) return NotFound("Login does not exist!");

          return Ok(login);
     }

     [HttpGet("logins")]
     public async Task<IActionResult> GetAllLogins()
     {
          ICollection<Login> logins = await _loginService.GetAllLogins();
          List<Login> loginsList = logins.ToList();

          if (loginsList.IsNullOrEmpty()) return NotFound("Not users have been created");

          return Ok(loginsList);
     }

     // [HttpGet("logins/{username}")]
     // public async Task<IActionResult> GetLoginByUsername(string username)
     // {

     //      try
     //      {
     //           Login? login = await _loginService.GetByUsername(username);

     //           return Ok(login);
     //      }
     //      catch (InvalidLoginException e)
     //      {
     //           return NotFound(e.Message);
     //      }
     // }
     
     // This is good for now but it is a bad practice, we are exposing the password resource through the URI
     // Maybe JWT(?) or a POST request to get password from the body (not a restful best practice).
     [HttpPost("login")]
     public async Task<IActionResult> GetLoginByUsernameAndPassword([FromBody] LoginDTO loginDTO)
     {
          try
          {
               Login? loginUsername = await _loginService.GetByUsername(loginDTO.Username);

               Login? login = await _loginService.GetByUsernameAndPassword(loginUsername!.Username, loginDTO.Password);

               return Ok(login!.UserID);

          }
          catch (InvalidLoginException e)
          {
               return NotFound(e.Message);
          }
     }

     [HttpPut("login/{ID}")]
     public async Task<IActionResult> UpdateLogin(int ID, [FromBody] LoginDTO updatedUser)
     {
          bool updatedLogin = await _loginService.UpdateLogin(ID, updatedUser);

          if (!updatedLogin) return NotFound($"An error has occurred when updating user with ID {ID}");

          return Ok("User succesfully updated");
     }

     [HttpDelete("login/{id}")]
     public async Task<IActionResult> DeleteLogin(int id)
     {
          try
          {
               Login? login = await _loginService.DeleteLogin(id);
               return Ok(login);

          }
          catch (InvalidLoginException e)
          {
               return NotFound(e.Message);
          }
     }
}