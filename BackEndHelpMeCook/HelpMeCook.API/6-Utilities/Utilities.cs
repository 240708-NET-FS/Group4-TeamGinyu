using HelpMeCook.API.Models;

namespace HelpMeCook.API.Utilities;

public static class UserUtility
{
    public static User DTOToUser(UserDTO userDTO)
    {
        User newUser = new()
        {
            FirstName = userDTO.FirstName, 
            LastName = userDTO.LastName,
            CratedDate = userDTO.CratedDate
        };
        return newUser;
    }
}

public static class LoginUtility
{
    public static Login DTOToLogin(LoginDTO loginDTO)
    {
        Login newLogin = new()
        {
            Username = loginDTO.Username, 
            Password = loginDTO.Password,
            UserID = loginDTO.UserID
        };
        return newLogin;
    }
}

public static class RecipeUtility
{
    public static Recipe DTOToRecipe(RecipeDTO recipeDTO)
    {
        Recipe newRecipe = new()
        {
            RecipeName = recipeDTO.RecipeName, 
            CratedDate = recipeDTO.CratedDate,
            RecipeNumber = recipeDTO.RecipeNumber,
            UserID = recipeDTO.UserID
        };
        return newRecipe;
    }
}