using HelpMeCook.API.DAO;
using HelpMeCook.API.Models;
using rUtil = HelpMeCook.API.Utilities.RecipeUtility;

namespace HelpMeCook.API.Services;

public class RecipeService : IRecipeService
{
    private readonly RecipeRepo _recipeRepo;

    public RecipeService(RecipeRepo recipeRepo)
    {
        _recipeRepo = recipeRepo;
    }

    public async Task<Recipe> CreateRecipe(RecipeDTO recipe)
    {
        Recipe newRecipe = rUtil.DTOToRecipe(recipe);
        return await _recipeRepo.Create(newRecipe);
    }

    public async Task<Recipe?> GetByID(int id)
    {
        if (id < 1) throw new ArgumentException("Invalid ID");
        return await _recipeRepo.GetByID(id);
    }

    public async Task<ICollection<Recipe>> GetAllRecipes()
    {
        return await _recipeRepo.GetAll()!;
    }

    public async Task<bool> Update(RecipeDTO recipe)
    {
        Recipe recipeToUpdate = rUtil.DTOToRecipe(recipe);
        return await _recipeRepo.Update(recipeToUpdate);    
    }

    public async Task<Recipe?> Delete(RecipeDTO recipe)
    {
        Recipe recipetoDelete = rUtil.DTOToRecipe(recipe);
        return await _recipeRepo.Delete(recipetoDelete);
    }
}