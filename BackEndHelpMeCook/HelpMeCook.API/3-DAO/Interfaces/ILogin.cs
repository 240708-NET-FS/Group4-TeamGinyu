namespace HelpMeCook.API.DAO.Interfaces;

public interface ILogin<T>
{
    // Create
    public void Create(T item);

    // Read
    public Task<T>? GetByID(int ID);

    public Task<T>? GetByUsername(string username);

    public Task<T> GetByUsernameAndPassword(string username, string password);

    public Task<ICollection<T>>? GetAll();

    // Update
    public Task<bool> Update(T newItem);

    // Delete
    public void Delete(T item);
}