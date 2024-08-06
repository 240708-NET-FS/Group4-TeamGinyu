using HelpMeCook.API.Models;

namespace HelpMeCook.API.DAO.Interfaces;

public interface IUserRepo
{
    // Create
    public Task<User> Create(User item);

    // Read
    public Task<User?> GetByID(string ID);

    public Task<ICollection<User>>? GetAll();

    // Update
    public Task<bool> Update(string ID, User newItem);

    // Delete
    public Task<User> Delete(string ID);
}