using HelpMeCook.API.Models;

namespace HelpMeCook.API.DAO.Interfaces;

public interface IRecipeRepo
{
    // Create
    public Task<Recipe> Create(Recipe item);

    // Read
    public Task<Recipe?> GetByID(int ID);

    public Task<Recipe?> GetByUser(int ID);

    public Task<Recipe?> GetByRecipeNumber(int ID);

    public Task<Recipe?> GetByRecipeName(string recipeName);
    public Task<ICollection<Recipe>> GetByRecipeNameAndUserID(string recipeName, int UserID);

    public Task<ICollection<Recipe>> GetAll();

    // Update
    public Task<bool> Update(int ID, Recipe newItem);

    // Delete
    public Task<Recipe> Delete(int ID);
}