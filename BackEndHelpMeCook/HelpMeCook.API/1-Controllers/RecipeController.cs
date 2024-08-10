using System.Security.Claims;
using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpMeCook.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        this._recipeService = recipeService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateRecipe(RecipeDTO recipeDTO)
    {
        try
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            recipeDTO.UserID = userID!;
            recipeDTO.CreatedDate = DateTime.Now;
            Recipe recipe = await _recipeService.CreateRecipe(recipeDTO);
            return Ok(recipe);

        }
        catch (InvalidRecipeException e)
        {
            return NotFound(e.Message);
        }



    }

    [HttpGet("recipe/{ID}"), Authorize]
    public async Task<IActionResult> GetRecipeByRecipeID(int ID)
    {
        try
        {

            Recipe? recipe = await _recipeService.GetRecipeById(ID);

            return Ok(recipe);
        }
        catch (InvalidRecipeException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("recipes"), Authorize]
    public async Task<IActionResult> GetAllRecipes()
    {
        try
        {
            List<Recipe> recipes = (List<Recipe>)await _recipeService.GetAllRecipes();

            return Ok(recipes);

        }
        catch (InvalidRecipeException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("recipe/user"), Authorize]
    public async Task<IActionResult> GetRecipeByUserID()
    {
        try
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ICollection<Recipe> recipes = await _recipeService.GetByUser(userID!);

            return Ok(recipes.ToList());

        }
        catch (InvalidRecipeException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("recipeNumber/{recipeNumber}"), Authorize]
    public async Task<IActionResult> GetByRecipeNumber(int recipeNumber)
    {
        try
        {
            Recipe? recipe = await _recipeService.GetByRecipeNumber(recipeNumber);

            return Ok(recipe);
        }
        catch (InvalidRecipeException e)
        {
            return NotFound(e.Message);
        }

    }

    [HttpGet("recipeName/{recipeName}"), Authorize]
    public async Task<IActionResult> GetByRecipeNumber(string recipeName)
    {
        try
        {
            Recipe? recipe = await _recipeService.GetByRecipeName(recipeName);

            return Ok(recipe);
        }
        catch (InvalidRecipeException e)
        {
            return NotFound(e.Message);
        }

    }

    [HttpGet("recipes/recipe"), Authorize]
    public async Task<IActionResult> GetByRecipeNameAndUserID([FromQuery] string recipeName, [FromQuery] string UserID)
    {
        try
        {
            Recipe? recipe = await _recipeService.GetByRecipeNameAndUserID(recipeName, UserID);

            return Ok(recipe);

        }
        catch (InvalidRecipeException e)
        {
            return NotFound(e.Message);
        }

    }

    [HttpPut("recipe/{ID}"), Authorize]
    public async Task<IActionResult> UpdateRecipe(int ID, [FromBody] RecipeDTO recipeDTO)
    {
        bool updatedRecipe = await _recipeService.Update(ID, recipeDTO);

        return updatedRecipe ? Ok("Recipe succesfully updated.") : NotFound($"Error when updating recipe with ID {ID}");
    }

    [HttpDelete("recipe/{ID}"), Authorize]
    public async Task<IActionResult> DeleteRecipe(int ID)
    {
        try
        {
            Recipe? recipeToDelete = await _recipeService.Delete(ID);

            return Ok(recipeToDelete);

        }
        catch (InvalidRecipeException e)
        {
            return NotFound(e.Message);
        }


    }

}