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
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        Recipe recipe = new Recipe { UserID = 1, RecipeID = 1, RecipeName = "MyRecipe", CratedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = 1, RecipeName = "MyRecipe", CratedDate = DateTime.Parse("07-06-1998") };

        recipeRepo.Setup(r => r.Create(It.IsAny<Recipe>())).ReturnsAsync(recipe);

        // Act
        Recipe recipeResult = await _recipeService.CreateRecipe(recipeDTO);

        // Assert
        Assert.Equal(recipe.RecipeID, recipeResult.RecipeID);
        Assert.Equal(recipe.RecipeName, recipeResult.RecipeName);
        Assert.Equal(recipe.CratedDate, recipeResult.CratedDate);
        Assert.Equal(recipe.UserID, recipeResult.UserID);
    }

    [Fact]
    public async void CreateRecipe_ShouldThrowExceptionWhenRecipeNameTaken()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        Recipe recipe = new Recipe { UserID = 1, RecipeID = 1, RecipeName = "MyRecipe", RecipeNumber = 1, CratedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = 1, RecipeName = "MyRecipe", RecipeNumber = 2, CratedDate = DateTime.Parse("07-06-1998") };

        recipeRepo.Setup(r => r.GetByRecipeName(It.IsAny<string>())).ReturnsAsync(recipe);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.CreateRecipe(recipeDTO));
    }

    [Fact]
    public async void CreateRecipe_ShouldThrowExceptionWhenRecipeNumberTaken()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        Recipe recipe = new Recipe { UserID = 1, RecipeID = 1, RecipeName = "MyRecipe", RecipeNumber = 1, CratedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = 1, RecipeName = "MyRecipe2", RecipeNumber = 1, CratedDate = DateTime.Parse("07-06-1998") };

        recipeRepo.Setup(r => r.GetByRecipeName(It.IsAny<string>())).ReturnsAsync(recipe);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.CreateRecipe(recipeDTO));
    }

    [Fact]
    public async void GetRecipeByID_ShouldReturnArgumentException()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);


        await Assert.ThrowsAsync<ArgumentException>(async () => await _recipeService.GetRecipeById(0));

    }

    [Fact]
    public async void GetRecipeByID_ShouldSuccesfullyGetRecipe()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);


        Recipe recipe = new Recipe { UserID = 1, RecipeID = 1, RecipeName = "MyRecipe", RecipeNumber = 1, CratedDate = DateTime.Parse("07-06-1998") };

        recipeRepo.Setup(r => r.GetByID(It.IsAny<int>())).ReturnsAsync(recipe);

        // Act
        Recipe? res = await _recipeService.GetRecipeById(1);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(recipe.RecipeID, res.RecipeID);
        Assert.Equal(recipe.UserID, res.UserID);
        Assert.Equal(recipe.RecipeName, res.RecipeName);
        Assert.Equal(recipe.RecipeNumber, res.RecipeNumber);
        Assert.Equal(recipe.CratedDate, res.CratedDate);
    }
}