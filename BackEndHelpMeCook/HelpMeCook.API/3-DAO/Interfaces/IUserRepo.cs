using HelpMeCook.API.Models;

namespace HelpMeCook.API.DAO.Interfaces;

public interface IUserRepo
{
    // Create
    public Task<User> Create(User item);

    // Read
    public Task<User?> GetByID(int ID);

    public Task<ICollection<User>>? GetAll();

    // Update
    public Task<bool> Update(User newItem);

    // Delete
    public Task<User> Delete(User item);
}