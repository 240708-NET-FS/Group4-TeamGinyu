using HelpMeCook.API.Models;

namespace HelpMeCook.API.Services
{
    public interface IUserService
    {
        // Create
        Task<User> CreateUser(UserDTO newUser);

        // Read
        Task<User?> GetUserByID(int userID);

        Task<ICollection<User>> GetAllUsers();

        // Update
        Task<bool> UpdateUser(UserDTO userToUpdate);

        // Delete
        Task<User?> DeleteUser(UserDTO userToDelete);
    }

    public interface ILoginService
    {
        // Create
        Task<Login> CreateLogin(LoginDTO newLogin);

        // Read
        Task<Login?> GetLoginByID(int loginID);

        Task<ICollection<Login>> GetAllLogins();

        // Update
        Task<Login?> UpdateLogin(LoginDTO newLogin);

        // Delete
        Task<Login?> DeleteLogin(LoginDTO loginToDelete);
    }

    public interface IRecipeService
    {
        // Create
        Task <Recipe> CreateRecipe(RecipeDTO newRecipe);
        Task <Recipe> CreateRecipe(RecipeDTO newRecipe);

        // Read
        Task<Recipe?> GetRecipeById(int recipeID);

        Task<ICollection<Recipe>> GetAllRecipes();

        // Update
        Task<User?> Update(RecipeDTO newRecipe);

        // Delete
        Task<User?> Delete(RecipeDTO recipeToDelete);
    }
}