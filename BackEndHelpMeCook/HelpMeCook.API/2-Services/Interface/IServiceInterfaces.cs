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
        Task<Login> CreateLogin(Login newLogin);

        // Read
        Task<Login?> GetLoginByID(int loginID);

        Task<ICollection<Login>> GetAllLogins();

        // Update
        Task<Login?> UpdateLogin(Login newLogin);

        // Delete
        Task<Login?> DeleteLogin(Login loginToDelete);
    }

    public interface IRecipeService
    {
        // Create
        Task <Recipe> CreateRecipe(Recipe newRecipe);

        // Read
        Task<Recipe?> GetByID(int recipeID);

        Task<ICollection<Recipe>> GetAllRecipes();

        // Update
        Task<User?> Update(Recipe newRecipe);

        // Delete
        Task<User?> Delete(Recipe recipeToDelete);
    }
}