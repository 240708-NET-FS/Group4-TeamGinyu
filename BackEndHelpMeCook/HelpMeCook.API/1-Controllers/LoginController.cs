
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