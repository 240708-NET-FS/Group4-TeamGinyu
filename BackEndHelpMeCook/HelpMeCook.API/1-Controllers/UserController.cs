using System.Security.Claims;
using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HelpMeCook.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
     private readonly IUserService _userService;

     public UserController(IUserService userService)
     {
          this._userService = userService;
     }

     [HttpPost("register")]
     public async Task<IActionResult> CreateUser(UserDTO userDTO)
     {
          var user = await _userService.CreateUser(userDTO);

          if (user.Succeeded)
          {
               return Ok($"User has been succesfully created.\n {user}");
          }

          return BadRequest(user.Errors);
     }

     [HttpPost("login")]
     public async Task<IActionResult> Login(UserDTO userLogin)
     {
          var user = await _userService.LoginUser(userLogin);

          if (user.Succeeded)
          {
               return Ok($"User logged in succesfully.\n {user}");
          }

          return Unauthorized("Invalid credentials.");
     }

     [HttpPost("userInfo"), Authorize]
     public IActionResult GetUserInfo()
     {
          var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

          if (userID.IsNullOrEmpty())
          {
               return Unauthorized();
          }

          return Ok(new { UserID = userID });
     }

     [HttpPost("logout")]
     public async Task<IActionResult> Logout()
     {
          await _userService.LogoutUser();

         return Ok("User logged out succesfully");
     }

     [HttpGet("user/{id}"), Authorize]
     public async Task<IActionResult> GetUserByID(string id)
     {
          var user = await _userService.GetUserByID(id);

          if (user is null) return NotFound("User does not exist!");
          return Ok(user);
     }

     [HttpGet("users"), Authorize]
     public async Task<IActionResult> GetAllUsers()
     {
          ICollection<User> users = await _userService.GetAllUsers();
          List<User> userList = users.ToList();

          if (userList.IsNullOrEmpty())
          {
               return NotFound("User does not exist!");
          }

          return Ok(userList);
     }

     [HttpPut("user/{id}"), Authorize]
     public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO updatedUser)
     {
          bool isUpdated = await _userService.UpdateUser(id, updatedUser);

          if (!isUpdated) return NotFound($"An error has occurred when updating user with ID {id}");

          return Ok("User succesfully updated");
     }

     [HttpDelete("user/{id}"), Authorize]
     public async Task<IActionResult> DeleteUser(string id)
     {
          try
          {
               User? user = await _userService.DeleteUser(id);
               return Ok(user);

          }
          catch (InvalidUserException e)
          {
               return NotFound(e.Message);
          }
     }
}