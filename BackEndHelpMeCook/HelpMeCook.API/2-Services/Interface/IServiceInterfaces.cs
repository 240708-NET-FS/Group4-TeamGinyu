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
        Task<bool> UpdateUser(int ID, UserDTO userToUpdate);

        // Delete
        Task<User?> DeleteUser(int ID);
    }

    public interface ILoginService
    {
        // Create
        Task<Login> CreateLogin(LoginDTO newLogin);

        // Read
        Task<Login?> GetLoginByID(int loginID);

        Task<ICollection<Login>> GetAllLogins();

        public Task<Login?> GetByUsername(string username);

        public Task<Login?> GetByUsernameAndPassword(string username, string password);

        // Update
        Task<bool> UpdateLogin(int ID, LoginDTO newLogin);

        // Delete
        Task<Login?> DeleteLogin(int ID);
    }

    public interface IRecipeService
    {
        // Create
        Task<Recipe> CreateRecipe(RecipeDTO newRecipe);

        // Read
        Task<Recipe?> GetRecipeById(int recipeID);

        public Task<ICollection<Recipe>> GetByUser(int ID);

        public Task<Recipe?> GetByRecipeNumber(int ID);

        public Task<Recipe?> GetByRecipeName(string recipeName);

        public Task<Recipe?> GetByRecipeNameAndUserID(string recipeName, int UserID);

        Task<ICollection<Recipe>> GetAllRecipes();

        // Update
        Task<bool> Update(int ID, RecipeDTO newRecipe);

        // Delete
        Task<Recipe?> Delete(int ID);
    }
}