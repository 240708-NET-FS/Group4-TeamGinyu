
using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using tUtil = HelpMeCook.API.Utilities;

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

     [HttpPost]
     public async Task<IActionResult> CreateLogin(LoginDTO loginDTO)
     {
          Login login = await _loginService.CreateLogin(loginDTO);

          return Ok(login);
     }

     [HttpGet("{id}")]
     public async Task<IActionResult> GetLoginByID(int id)
     {
          var login = await _loginService.GetLoginByID(id);

          if (login is null) return NotFound("Login does not exist!");

          return Ok(login);
     }

     [HttpGet("/logins")]
     public async Task<IActionResult> GetAllLogins()
     {
          ICollection<Login> logins = await _loginService.GetAllLogins();
          List<Login> loginsList = logins.ToList();

          if (loginsList.IsNullOrEmpty()) return NotFound("Not users have been created");

          return Ok(_loginService.GetAllLogins());
     }

     [HttpGet("/logins/{username}")]
     public async Task<IActionResult> GetLoginByUsername(string username)
     {

          try
          {
               Login? login = await _loginService.GetByUsername(username);

               return Ok(login);
          }
          catch (InvalidLoginException e)
          {
               return NotFound(e.Message);
          }
     }
     
     // This is good for now but it is a bad practice, we are exposing the password resource through the URI
     // Maybe JWT(?) or a POST request to get password from the body (not a restful best practice).
     [HttpGet("/logins/userpassword")]
     public async Task<IActionResult> GetLoginByUsernameAndPassword([FromQuery] string username, [FromQuery] string password)
     {
          try
          {
               Login? login = await _loginService.GetByUsernameAndPassword(username, password);
               return Ok(login);

          }
          catch (InvalidLoginException e)
          {
               return NotFound(e.Message);
          }
     }

     [HttpPut("/login/update/{id}")]
     public async Task<IActionResult> UpdateLogin(int ID, [FromBody] LoginDTO updatedUser)
     {
          bool updatedLogin = await _loginService.UpdateLogin(ID, updatedUser);

          if (!updatedLogin) return NotFound($"An error has occurred when updating user with ID {ID}");

          return Ok("User succesfully updated");
     }

     [HttpDelete("/login/delete/{id}")]
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