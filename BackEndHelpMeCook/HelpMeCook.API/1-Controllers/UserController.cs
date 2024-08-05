using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
   public async Task<IActionResult> CreateUser (UserDTO userDTO)
   {
        User user = await _userService.CreateUser(userDTO);

        return Ok(user);
   }

   [HttpGet("user/{id}")]
   public async Task<IActionResult> GetUserByID (int id)
   {
        var user = await _userService.GetUserByID(id);

        if(user is null) return NotFound("User does not exist!");
        return Ok(user); 
   }

    [HttpGet("users")]
   public async Task<IActionResult> GetAllUsers ()
   {
        ICollection<User> users = await _userService.GetAllUsers();
        List<User> userList = users.ToList();

        if(userList.IsNullOrEmpty())
        {
            return NotFound("User does not exist!");
        }

        return Ok(userList); 
   }

   [HttpPut("user/{id}")]
   public async Task<IActionResult> UpdateUser (int id, [FromBody] UserDTO updatedUser)
   {
        bool isUpdated = await _userService.UpdateUser(id, updatedUser);

        if(!isUpdated) return NotFound($"An error has occurred when updating user with ID {id}");

        return Ok("User succesfully updated");
   }

   [HttpDelete("user/{id}")]
   public async Task<IActionResult> DeleteUser (int id)
   {
        try 
        {
            User? user = await _userService.DeleteUser(id);
            return Ok(user);

        } catch (InvalidUserException e)
        {
            return NotFound(e.Message);
        }        
   }
}