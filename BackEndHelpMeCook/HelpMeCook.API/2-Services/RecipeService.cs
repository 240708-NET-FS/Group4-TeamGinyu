using HelpMeCook.API.DAO;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Models;
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
        return await _recipeRepo.Create(newRecipe);
    }

    public async Task<Recipe?> GetRecipeById(int id)
    {
        if (id < 1) throw new ArgumentException("Invalid ID");
        return await _recipeRepo.GetByID(id);
    }

    public async Task<ICollection<Recipe>> GetAllRecipes()
    {
        return await _recipeRepo.GetAll()!;
    }

    public async Task<bool> Update(int ID, RecipeDTO recipe)
    {
        Recipe recipeToUpdate = rUtil.DTOToRecipe(recipe);

        return await _recipeRepo.Update(ID, recipeToUpdate);    
    }

    public async Task<Recipe?> Delete(int ID)
    {

        Recipe? recipe = await _recipeRepo.GetByID(ID);

        if(recipe == null)
        {
            throw new InvalidRecipeException("User does not exst.");
        }
        
        return await _recipeRepo.Delete(ID);
    }

}