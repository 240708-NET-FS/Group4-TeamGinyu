using System.Collections;
using HelpMeCook.API.DAO;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Models;
using Microsoft.IdentityModel.Tokens;
using rUtil = HelpMeCook.API.Utilities.RecipeUtility;

namespace HelpMeCook.API.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipeRepo _recipeRepo;

    public RecipeService(IRecipeRepo recipeRepo)
    {
        _recipeRepo = recipeRepo;
    }

    public async Task<Recipe> CreateRecipe(RecipeDTO recipe)
    {
        Recipe newRecipe = rUtil.DTOToRecipe(recipe);

        if(await RecipeNameTaken(newRecipe))
        {
            throw new InvalidRecipeException($"Recipe Name {recipe.RecipeName} already taken");
        }

        if(await RecipeNumberRegistered(newRecipe))
        {
            throw new InvalidRecipeException($"Recipe Number {recipe.RecipeNumber} already registered.");
        }

        return await _recipeRepo.Create(newRecipe);
    }

    public async Task<Recipe?> GetRecipeById(int id)
    {
        if (id < 1) throw new ArgumentException("Invalid ID");

        Recipe? recipe = await _recipeRepo.GetByID(id);

        if(recipe == null)
        {
            throw new InvalidRecipeException($"Recipe with ID {id} could not be found.");
        }

        return recipe;
    }

    public async Task<ICollection<Recipe>> GetAllRecipes()
    {
        ICollection<Recipe> recipesCol = await _recipeRepo.GetAll();
        List<Recipe> recipesList = recipesCol.ToList();

        if(recipesList.IsNullOrEmpty())
        {
            throw new InvalidRecipeException($"No recipes found.");
        }

        return recipesList;
    }

    public async Task<ICollection<Recipe>> GetByUser(string UserID)
    {
        ICollection<Recipe> recipes = await _recipeRepo.GetByUser(UserID);
        List<Recipe> recipesList = recipes.ToList();

        if (recipesList.IsNullOrEmpty())
        {
            throw new InvalidRecipeException($"No recipe with User ID {UserID} could be found.");
        }

        return recipesList;
    }

    public async Task<Recipe?> GetByRecipeNumber(int RecipeID)
    {
        Recipe? recipe = await _recipeRepo.GetByRecipeNumber(RecipeID);

        if (recipe == null)
        {
            throw new InvalidRecipeException($"Recipe with ID {RecipeID} could not be found");
        }

        return recipe;
    }

    public async Task<Recipe?> GetByRecipeName(string recipeName)
    {
        Recipe? recipe = await _recipeRepo.GetByRecipeName(recipeName);

        if (recipe == null)
        {
            throw new InvalidRecipeException($"Recipe with name {recipeName} could not be found.");
        }

        return recipe;
    }

    public async Task<Recipe?> GetByRecipeNameAndUserID(string recipeName, string UserID)
    {
        Recipe? recipe = await _recipeRepo.GetByRecipeNameAndUserID(recipeName, UserID);
        

        if (recipe == null)
        {
            throw new InvalidRecipeException($"Recipe with name {recipeName} and User ID {UserID} could not be found.");
        }

        return recipe;
    }

    public async Task<bool> Update(int ID, RecipeDTO recipe)
    {
        Recipe recipeToUpdate = rUtil.DTOToRecipe(recipe);

        return await _recipeRepo.Update(ID, recipeToUpdate);
    }

    public async Task<Recipe?> Delete(int ID)
    {

        Recipe? recipe = await _recipeRepo.GetByID(ID);

        if (recipe == null)
        {
            throw new InvalidRecipeException("User does not exst.");
        }

        return await _recipeRepo.Delete(ID);
    }

    private async Task<bool> RecipeNameTaken(Recipe recipe) 
    {
        Recipe? dbRecipe =  await _recipeRepo.GetByRecipeName(recipe.RecipeName!);
        
        return dbRecipe != null;
    }

     private async Task<bool> RecipeNumberRegistered(Recipe recipe) 
    {
        Recipe? dbRecipe =  await _recipeRepo.GetByRecipeNumber(recipe.RecipeNumber!);
        
        return dbRecipe != null;
    }
}