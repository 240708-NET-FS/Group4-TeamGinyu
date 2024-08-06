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
        _context.Users.Add(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<User> Delete(string ID)
    {
       User user = _context.Users.Find(ID)!;
       _context.Users.Remove(user);
       await _context.SaveChangesAsync();

       return user;
    }

    public async Task<ICollection<User>>? GetAll()
    {
       return  await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByID(string ID)
    {
        return await _context.Users.FirstOrDefaultAsync(p => p.Id == ID);
    }

    public async Task<bool> Update(string ID, User newItem)
    {
        User? oldUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == ID);

        if(oldUser == null) 
        {
            return false;
        }
       
        oldUser.FirstName = newItem.FirstName;
        oldUser.LastName = newItem.LastName;

        _context.Users.Update(oldUser);
        await _context.SaveChangesAsync();

        return true;
    }

}