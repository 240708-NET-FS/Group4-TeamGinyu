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

        Recipe recipe = new Recipe { UserID = "1", RecipeID = 1, RecipeName = "MyRecipe", CreatedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = "1", RecipeName = "MyRecipe", CreatedDate = DateTime.Parse("07-06-1998") };

        recipeRepo.Setup(r => r.Create(It.IsAny<Recipe>())).ReturnsAsync(recipe);

        // Act
        Recipe recipeResult = await _recipeService.CreateRecipe(recipeDTO);

        // Assert
        Assert.Equal(recipe.RecipeID, recipeResult.RecipeID);
        Assert.Equal(recipe.RecipeName, recipeResult.RecipeName);
        Assert.Equal(recipe.CreatedDate, recipeResult.CreatedDate);
        Assert.Equal(recipe.UserID, recipeResult.UserID);
    }

    [Fact]
    public async void CreateRecipe_ShouldThrowExceptionWhenRecipeNameTaken()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        Recipe recipe = new Recipe { UserID = "1", RecipeID = 1, RecipeName = "MyRecipe", RecipeNumber = 1, CreatedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = "1", RecipeName = "MyRecipe", RecipeNumber = 2, CreatedDate = DateTime.Parse("07-06-1998") };

        recipeRepo.Setup(r => r.GetByRecipeName(It.IsAny<string>())).ReturnsAsync(recipe);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.CreateRecipe(recipeDTO));
    }

    [Fact]
    public async void CreateRecipe_ShouldThrowExceptionWhenRecipeNumberRegistered()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        Recipe recipe = new Recipe { UserID = "1", RecipeID = 1, RecipeName = "MyRecipe", RecipeNumber = 1, CreatedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = "1", RecipeName = "MyRecipe222", RecipeNumber = 1, CreatedDate = DateTime.Parse("07-06-1998") };

        recipeRepo.Setup(r => r.GetByRecipeNumber(It.IsAny<int>())).ReturnsAsync(recipe);

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


        Recipe recipe = new Recipe { UserID = "1", RecipeID = 1, RecipeName = "MyRecipe", RecipeNumber = 1, CreatedDate = DateTime.Parse("07-06-1998") };

        recipeRepo.Setup(r => r.GetByID(It.IsAny<int>())).ReturnsAsync(recipe);

        // Act
        Recipe? res = await _recipeService.GetRecipeById(1);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(recipe.RecipeID, res.RecipeID);
        Assert.Equal(recipe.UserID, res.UserID);
        Assert.Equal(recipe.RecipeName, res.RecipeName);
        Assert.Equal(recipe.RecipeNumber, res.RecipeNumber);
        Assert.Equal(recipe.CreatedDate, res.CreatedDate);
    }

    [Fact]
    public async void GetAllRecipe_ShouldReturnExceptionWhenEmptyList()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.GetAll()).ReturnsAsync(new List<Recipe>());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.GetAllRecipes());

    }

    [Fact]
    public async void GetAllRecipe_ShouldSuccesfullyReturnAllRecipes()
    {
        // Arrange
        List<Recipe> recipes = new List<Recipe>{
            new Recipe { UserID = "1", RecipeID = 1, RecipeName = "MyRecipe1", RecipeNumber = 1, CreatedDate = DateTime.Parse("07-06-1998") },
            new Recipe { UserID = "2", RecipeID = 2, RecipeName = "MyRecipe2", RecipeNumber = 2, CreatedDate = DateTime.Parse("07-06-1998") },
            new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") }
        };
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.GetAll()).ReturnsAsync(recipes);

        // Act
        ICollection<Recipe> res = await _recipeService.GetAllRecipes();

        // Assert
        Assert.NotNull(res);
        Assert.Equal(recipes, res);
    }

    [Fact]
    public async void GetByUser_ShouldReturnExceptionWhenEmptyList()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.GetByUser(It.IsAny<string>())).ReturnsAsync(new List<Recipe>());

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.GetByUser("1"));

    }

    [Fact]
    public async void GetByUser_ShouldSuccesfullyReturnListOfUserRecipes()
    {
        // Arrange
        List<Recipe> recipes = new List<Recipe>{
            new Recipe { UserID = "1", RecipeID = 1, RecipeName = "MyRecipe1", RecipeNumber = 1, CreatedDate = DateTime.Parse("07-06-1998") },
            new Recipe { UserID = "2", RecipeID = 2, RecipeName = "MyRecipe2", RecipeNumber = 2, CreatedDate = DateTime.Parse("07-06-1998") },
            new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") }
        };
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.GetByUser(It.IsAny<string>())).ReturnsAsync(recipes);

        // Act
        ICollection<Recipe> res = await _recipeService.GetByUser("1");

        // Assert
        Assert.NotNull(res);
        Assert.Equal(recipes, res);
    }

    [Fact]
    public async void GetByRecipeNumber_ShouldReturnExceptionWhenInvalidRecipeNumber()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.GetByRecipeNumber(1));

    }

    [Fact]
    public async void GetByRecipeNumber_ShouldSuccesfullyReturnRecipe()
    {
        // Arrange
        Recipe recipe = new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };

        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.GetByRecipeNumber(recipe.RecipeNumber)).ReturnsAsync(recipe);

        // Act
        Recipe? res = await _recipeService.GetByRecipeNumber(recipe.RecipeNumber);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(recipe.RecipeID, res.RecipeID);
        Assert.Equal(recipe.RecipeName, res.RecipeName);
        Assert.Equal(recipe.RecipeNumber, res.RecipeNumber);
        Assert.Equal(recipe.UserID, res.UserID);

    }

    [Fact]
    public async void GetByRecipeName_ShouldReturnExceptionWhenInvalidRecipeName()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.GetByRecipeName("Recipe"));

    }


    [Fact]
    public async void GetByRecipeName_ShouldSuccesfullyReturnRecipe()
    {
        // Arrange
        Recipe recipe = new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };

        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.GetByRecipeName(recipe.RecipeName)).ReturnsAsync(recipe);

        // Act & Assert
        Recipe? res = await _recipeService.GetByRecipeName(recipe.RecipeName);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(recipe.RecipeID, res.RecipeID);
        Assert.Equal(recipe.RecipeName, res.RecipeName);
        Assert.Equal(recipe.RecipeNumber, res.RecipeNumber);
        Assert.Equal(recipe.UserID, res.UserID);
    }

    [Fact]
    public async void GetByRecipeNameAndUserID_ShouldReturnExceptionWhenInvalidRecipeNameAndUserID()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.GetByRecipeNameAndUserID("Recipe", "1"));

    }

    [Fact]
    public async void GetByRecipeNameAndUserID_ShouldReturnExceptionWhenInvalidUserIDGiven()
    {
        // Arrange
        Recipe recipe = new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };

        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.GetByRecipeNameAndUserID(recipe.RecipeName, "3")).ReturnsAsync(recipe);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.GetByRecipeNameAndUserID(recipe.RecipeName, "1"));


    }

    [Fact]
    public async void GetByRecipeNameAndUserID_ShouldSuccesfullyReturnRecipe()
    {
        // Arrange
        Recipe recipe = new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };

        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.GetByRecipeNameAndUserID(recipe.RecipeName, "3")).ReturnsAsync(recipe);

        // Act & Assert
        Recipe? res = await _recipeService.GetByRecipeNameAndUserID(recipe.RecipeName, "3");

        // Assert
        Assert.NotNull(res);
        Assert.Equal(recipe.RecipeID, res.RecipeID);
        Assert.Equal(recipe.RecipeName, res.RecipeName);
        Assert.Equal(recipe.RecipeNumber, res.RecipeNumber);
        Assert.Equal(recipe.UserID, res.UserID);
    }

    [Fact]
    public async void Update_ShouldReturnFalseWhenInexistentRecipe()
    {
        // Arrange
        Recipe recipe = new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = "3", RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };

        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        await _recipeService.CreateRecipe(recipeDTO);

        // Act & Assert
        bool res = await _recipeService.Update(recipe.RecipeID, new RecipeDTO { UserID = "3", RecipeName = "newRecipe", RecipeNumber = 5, CreatedDate = DateTime.Parse("07-06-1998") });

        // Assert
        Assert.False(res);
    }

    [Fact]
    public async void Update_ShouldSuccesfullyUpdateRecipe()
    {
        // Arrange
        Recipe recipe = new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = "3", RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };

        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.Create(recipe)).ReturnsAsync(recipe);
        recipeRepo.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<Recipe>())).ReturnsAsync(true);


        // Act
        bool res = await _recipeService.Update(recipe.RecipeID, new RecipeDTO { UserID = "3", RecipeName = "newRecipe", RecipeNumber = 5, CreatedDate = DateTime.Parse("07-06-1998") });

        // Assert
        Assert.True(res);
    }

    [Fact]
    public async void Delete_ShouldReturnExceptionWhenInvalidID()
    {
        // Arrange
        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.Delete(1));
    }

    [Fact]
    public async void Delete_ShouldSuccesfullyDeleteUser()
    {
        // Arrange
        Recipe recipe = new Recipe { UserID = "3", RecipeID = 3, RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };
        RecipeDTO recipeDTO = new RecipeDTO { UserID = "3", RecipeName = "MyRecipe3", RecipeNumber = 3, CreatedDate = DateTime.Parse("07-06-1998") };

        Mock<IRecipeRepo> recipeRepo = new();
        RecipeService _recipeService = new RecipeService(recipeRepo.Object);

        recipeRepo.Setup(r => r.Create(It.IsAny<Recipe>())).ReturnsAsync(recipe);
        recipeRepo.Setup(r => r.Delete(3)).ReturnsAsync(recipe);
        recipeRepo.SetupSequence(r => r.GetByID(3)).ReturnsAsync(recipe).ReturnsAsync((Recipe?) null);

        await _recipeService.CreateRecipe(recipeDTO);

        // Act
        Recipe? res = await _recipeService.Delete(3);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(recipe.RecipeID, res.RecipeID);
        Assert.Equal(recipe.RecipeName, res.RecipeName);
        Assert.Equal(recipe.RecipeNumber, res.RecipeNumber);
        Assert.Equal(recipe.UserID, res.UserID);
        await Assert.ThrowsAsync<InvalidRecipeException>(async () => await _recipeService.GetRecipeById(3));

    }
}