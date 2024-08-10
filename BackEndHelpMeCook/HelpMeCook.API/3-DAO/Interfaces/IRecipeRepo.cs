using HelpMeCook.API.Models;

namespace HelpMeCook.API.DAO.Interfaces;

public interface IRecipeRepo
{
    // Create
    public Task<Recipe> Create(Recipe item);

    // Read
    public Task<Recipe?> GetByID(int ID);

    public Task<ICollection<Recipe>> GetByUser(string ID);

    public Task<Recipe?> GetByRecipeNumberAndUserID(int recipeNumber, string userId);
    public Task<Recipe?> GetByRecipeNumber(int recipeNumber);

    public Task<Recipe?> GetByRecipeName(string recipeName);

    public Task<Recipe?> GetByRecipeNameAndUserID(string recipeName, string UserID);

    public Task<ICollection<Recipe>> GetAll();

    // Update
    public Task<bool> Update(int ID, Recipe newItem);

    // Delete
    public Task<Recipe> Delete(int ID);
}