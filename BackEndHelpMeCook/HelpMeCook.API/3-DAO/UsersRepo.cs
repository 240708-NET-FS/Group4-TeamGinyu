using Microsoft.EntityFrameworkCore;
using HelpMeCook.API.Models;
using HelpMeCook.API.DAO.Interfaces;

namespace HelpMeCook.API.DAO;

public class UserRepo : IUser<User> {

    private readonly AppDbContext _context;

    public UserRepo(AppDbContext context) {
        this._context = context;
    }

    public void Create(User item)
    {
        throw new NotImplementedException();
    }

    public void Delete(User item)
    {
        throw new NotImplementedException();
    }

    public ICollection<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User GetByID(int ID)
    {
        //return _context.User.FirstOrDefault(p => p.UserId == id);
        throw new NotImplementedException();
    }

    public bool Update(User newItem)
    {
        throw new NotImplementedException();
    }
}