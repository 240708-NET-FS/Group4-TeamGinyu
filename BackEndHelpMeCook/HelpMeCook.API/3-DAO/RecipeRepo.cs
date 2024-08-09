
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpMeCook.API.DAO;

public class RecipeRepo : IRecipeRepo
{

    private readonly AppDbContext _context;

    public RecipeRepo(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<Recipe> Create(Recipe item)
    {
        _context.Recipe.Add(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<ICollection<Recipe>> GetAll()
    {
        return await _context.Recipe.Include(r => r.User).ToListAsync();
    }

    public async Task<Recipe?> GetByID(int ID)
    {
        return await _context.Recipe
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.RecipeID == ID);
    }

    public async Task<Recipe?> GetByRecipeName(string recipeName)
    {
        return await _context.Recipe
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.RecipeName == recipeName);
    }

    public async Task<Recipe?> GetByRecipeNameAndUserID(string recipeName, string UserID)
    {
        return await _context.Recipe
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.RecipeName == recipeName && r.UserID == UserID);
    }

    public async Task<Recipe?> GetByRecipeNumber(int ID)
    {
        return await _context.Recipe
                 .Include(r => r.User)
                 .FirstOrDefaultAsync(r => r.RecipeNumber == ID);
    }

    public async Task<ICollection<Recipe>> GetByUser(string ID)
    {

        return await _context.Recipe
                .Where(r => r.UserID == ID)
                .ToListAsync();

    }

    public async Task<bool> Update(int ID, Recipe newItem)
    {
        Recipe? oldRecipe = await _context.Recipe.FirstOrDefaultAsync(r => r.RecipeID == ID);

        if (oldRecipe == null)
        {
            return false;
        }

        oldRecipe.RecipeName = newItem.RecipeName;
        oldRecipe.RecipeNumber = newItem.RecipeNumber;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Recipe> Delete(int ID)
    {
        Recipe recipe = _context.Recipe.Find(ID)!;

        _context.Recipe.Remove(recipe);
        await _context.SaveChangesAsync();

        return recipe;
    }

}