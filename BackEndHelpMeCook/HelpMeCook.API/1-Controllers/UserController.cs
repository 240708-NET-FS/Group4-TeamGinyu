
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Mvc;
using tUtil = HelpMeCook.API.Utilities;

namespace HelpMeCook.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
   
   public UserController(IUserService userService)
   {
        this._userService = userService;
   }

   [HttpPost]
   public async Task<User> CreateUser (UserDTO userDTO)
   {
        return await _userService.CreateUser(userDTO);
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetUserByID (int id)
   {
        var user = _userService.GetUserByID(id);

        if(user is null) return NotFound("User does not exist!");
        return Ok(user); 
   }

    [HttpGet("/users")]
   public async Task<IActionResult> GetAllUsers ()
   {
        return Ok(_userService.GetAllUsers()); 
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