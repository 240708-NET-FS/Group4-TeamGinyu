namespace HelpMeCook.API.DAO.Interfaces;

public interface ILogin<T>
{
    // Create
    public void Create(T item);

    // Read
    public T GetByID(int ID);

    public T GetByUsername(string username);

    public T GetByUsernameAndPassword(string username, string password);

    public ICollection<T> GetAll();

    // Update
    public bool Update(T newItem);

    // Delete
    public void Delete(T item);
}