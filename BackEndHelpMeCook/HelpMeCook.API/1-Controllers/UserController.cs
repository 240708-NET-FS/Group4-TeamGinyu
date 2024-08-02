
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpMeCook.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    private readonly IUserService _userService;
   
   public UserController(IUserService userService)
   {
    this._userService = userService;
   }

   [HttpPost]
   public async Task<IActionResult> CreateUser (UserDTO userDTO)
   {
        throw new NotImplementedException();
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetUserByID (int id)
   {
        throw new NotImplementedException();
   }

    [HttpGet("/users")]
   public async Task<IActionResult> GetAllUsers ()
   {
        throw new NotImplementedException();
   }

   [HttpPut]
   public async Task<IActionResult> UpdateUser ([FromBody] UserDTO updatedUser)
   {
        throw new NotImplementedException();
   }

   [HttpDelete]
   public async Task<IActionResult> DeleteUser ([FromBody] UserDTO userToDelete)
   {
        throw new NotImplementedException();
   }
}