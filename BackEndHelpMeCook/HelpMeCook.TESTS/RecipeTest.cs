using Moq;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Services;
using HelpMeCook.API.Models;
using HelpMeCook.API.Exceptions;

namespace HelpMeCook.Tests;

public class RecipeTest
{
    [Fact]
    public async void CreateRecipe_ShouldSuccesfullyCreateRecipe()
    {
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        Recipe recipe = new Recipe { UserID = 1, RecipeID = 1, RecipeName = "MyRecipe", CratedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = 1, RecipeName = "MyRecipe", CratedDate = DateTime.Parse("07-06-1998") };


        recipeRepo.Setup(r => r.Create(It.IsAny<Recipe>())).ReturnsAsync(recipe);

        Recipe recipeResult = await _recipeService.CreateRecipe(recipeDTO);

        Assert.Equal(recipe.RecipeID, recipeResult.RecipeID);
        Assert.Equal(recipe.RecipeName, recipeResult.RecipeName);
        Assert.Equal(recipe.CratedDate, recipeResult.CratedDate);
        Assert.Equal(recipe.UserID, recipeResult.UserID);
    }

    [Fact]
    public async void CreateRecipe_ShouldThrowExceptionWhenRecipeNameTaken()
    {
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        // Recipe recipe = new Recipe { UserID = 1, RecipeID = 1, RecipeName = "MyRecipe", CratedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = 1, RecipeName = "MyRecipe", CratedDate = DateTime.Parse("07-06-1998") };

        // Recipe recipe2 = new Recipe { UserID = 1, RecipeID = 2, RecipeName = "MyRecipe", CratedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO2 = new RecipeDTO { UserID = 1, RecipeName = "MyRecipe", CratedDate = DateTime.Parse("07-06-1998") };

        await _recipeService.CreateRecipe(recipeDTO);        

        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.CreateRecipe(recipeDTO2));
    }
}