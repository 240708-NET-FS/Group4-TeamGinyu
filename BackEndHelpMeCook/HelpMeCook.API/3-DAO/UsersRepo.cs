using Microsoft.EntityFrameworkCore;
using HelpMeCook.API.Models;
using HelpMeCook.API.DAO.Interfaces;

namespace HelpMeCook.API.DAO;

public class UserRepo : IUserRepo {

    private readonly AppDbContext _context;

    public UserRepo(AppDbContext context) {
        this._context = context;
    }

    public async Task<User> Create(User item)
    {
        _context.User.Add(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<User> Delete(User item)
    {
       _context.User.Remove(item);
       await _context.SaveChangesAsync();

       return item;
    }

    public async Task<ICollection<User>>? GetAll()
    {
       return  await _context.User.ToListAsync();
    }

    public async Task<User?> GetByID(int ID)
    {
        return await _context.User.FirstOrDefaultAsync(p => p.UserID == ID);
    }

    public async Task<bool> Update(User newItem)
    {
        User? oldUser = await _context.User.FirstOrDefaultAsync(p => p.UserID == newItem.UserID);

        if(oldUser == null) 
        {
            return false;
        }
       
        oldUser.FirstName = newItem.FirstName;
        oldUser.LastName = newItem.LastName;

        _context.User.Update(oldUser);
        await _context.SaveChangesAsync();

        return true;
    }
}