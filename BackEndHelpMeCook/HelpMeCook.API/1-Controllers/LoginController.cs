
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Mvc;
using tUtil = HelpMeCook.API.Utilities;

namespace HelpMeCook.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class LoginController: ControllerBase
{
    private readonly ILoginService _loginService;
   
   public LoginController(ILoginService loginService)
   {
        this._loginService = loginService;
   }

   [HttpPost]
   public async Task<Login> CreateLogin (LoginDTO loginDTO)
   {
        return await _loginService.CreateLogin(loginDTO);
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetUserByID (int id)
   {
        var login = _loginService.GetLoginByID(id);

        if(login is null) return NotFound("Login does not exist!");
        return Ok(login);  
   }

    [HttpGet("/logins")]
   public async Task<IActionResult> GetAllUsers ()
   {
        return Ok(_loginService.GetAllLogins()); 
   }

   [HttpPut]
   public async Task<IActionResult> UpdateUser ([FromBody] LoginDTO updatedUser)
   {
        throw new NotImplementedException();
   }

   [HttpDelete]
   public async Task<IActionResult> DeleteUser ([FromBody] LoginDTO userToDelete)
   {
        throw new NotImplementedException();
   }
}