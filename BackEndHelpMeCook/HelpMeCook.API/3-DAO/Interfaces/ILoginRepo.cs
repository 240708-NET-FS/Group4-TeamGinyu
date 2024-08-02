using HelpMeCook.API.Models;

namespace HelpMeCook.API.DAO.Interfaces;

public interface ILoginRepo
{
    // Create
    public Task<Login> Create(Login item);

    // Read
    public Task<Login?> GetByID(int ID);

    public Task<Login?> GetByUsername(string username);

    public Task<Login?> GetByUsernameAndPassword(string username, string password);

    public Task<ICollection<Login>> GetAll();

    // Update
    public Task<bool> Update(Login newItem);

    // Delete
    public Task<Login> Delete(Login item);
}