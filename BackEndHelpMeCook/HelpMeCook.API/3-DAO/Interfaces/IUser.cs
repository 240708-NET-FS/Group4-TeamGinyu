namespace HelpMeCook.API.DAO.Interfaces;

public interface IUser<T>
{
    // Create
    public T Create(T item);

    // Read
    public Task<T?> GetByID(int ID);

    public Task<ICollection<T>>? GetAll();

    // Update
    public Task<bool> Update(T newItem);

    // Delete
    public T Delete(T item);
}