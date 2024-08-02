namespace HelpMeCook.API.DAO.Interfaces;

public interface IRecipe<T>
{
    // Create
    public void Create(T item);

    // Read
    public Task<T?> GetByID(int ID);

    public Task<T?> GetByUser(int ID);

    public Task<T?> GetByRecipeNumber(int ID);

    public Task<T?> GetByRecipeName(string recipeName);
    public Task<T?> GetByRecipeNameAndUserID(string recipeName, int UserID);

    public Task<ICollection<T>> GetAll();

    // Update
    public Task<bool> Update(T newItem);

    // Delete
    public void Delete(T item);
}