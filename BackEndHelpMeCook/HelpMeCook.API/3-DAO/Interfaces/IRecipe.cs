namespace HelpMeCook.API.DAO.Interfaces;

public interface IRecipe<T>
{
    // Create
    public void Create(T item);

    // Read
    public T GetByID(int ID);

    public T GetByUser(int ID);

    public T GetByRecipeId(int ID);

    public T GetByRecipeName(string recipeName);

    public ICollection<T> GetAll();

    // Update
    public bool Update(T newItem);

    // Delete
    public void Delete(T item);
}