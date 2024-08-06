using HelpMeCook.API.Models;
using Microsoft.AspNetCore.Identity;

namespace HelpMeCook.API.Services
{
    public interface IUserService
    {
        // Create
        Task<IdentityResult> CreateUser(UserDTO newUser);

        // Read
        Task<User?> GetUserByID(string userID);

        public Task<SignInResult> LoginUser(UserDTO loginDto);

        public Task LogoutUser();

        Task<ICollection<User>> GetAllUsers();

        // Update
        Task<bool> UpdateUser(string ID, UserDTO userToUpdate);

        // Delete
        Task<User?> DeleteUser(string ID);
    }

    public interface IRecipeService
    {
        // Create
        Task<Recipe> CreateRecipe(RecipeDTO newRecipe);

        // Read
        Task<Recipe?> GetRecipeById(int recipeID);

        public Task<ICollection<Recipe>> GetByUser(string ID);

        public Task<Recipe?> GetByRecipeNumber(int ID);

        public Task<Recipe?> GetByRecipeName(string recipeName);

        public Task<Recipe?> GetByRecipeNameAndUserID(string recipeName, string UserID);

        Task<ICollection<Recipe>> GetAllRecipes();

        // Update
        Task<bool> Update(int ID, RecipeDTO newRecipe);

        // Delete
        Task<Recipe?> Delete(int ID);
    }
}