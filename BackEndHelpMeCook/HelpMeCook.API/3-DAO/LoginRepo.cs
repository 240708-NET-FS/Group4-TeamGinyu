
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpMeCook.API.DAO;

public class LoginRepo : ILoginRepo
{
    private readonly AppDbContext _context;

    public LoginRepo(AppDbContext context) {
        this._context = context;
    }

    public async Task<Login> Create(Login item)
    {
        _context.Login.Add(item);
        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<Login> Delete(int ID)
    {
        Login? login = await _context.Login.Include(l => l.User).FirstOrDefaultAsync(l => l.LoginID == ID);

        _context.Login.Remove(login!);
        await _context.SaveChangesAsync();

        return login!;
    }

    public async Task<ICollection<Login>> GetAll()
    {
        return await _context.Login.Include(u => u.User).ToListAsync();
    }

    public async Task<Login?> GetByID(int ID)
    {
        return await _context.Login.Include(l => l.User).FirstOrDefaultAsync(l => l.LoginID == ID);
    }

    public async Task<Login?> GetByUsername(string username)
    {
        return await _context.Login.Include(l => l.User).FirstOrDefaultAsync(l => l.Username == username);
    }

    public async Task<Login?> GetByUsernameAndPassword(string username, string password)
    {
        // Strech goal 
        // Query username and analyse hash password.
        return await _context.Login.Include(l => l.User).FirstOrDefaultAsync(l => l.Username == username && l.Password == password);
    }

    public async Task<bool> Update(int ID, Login newItem)
    {
        Login? oldLogin = await _context.Login.FirstOrDefaultAsync(l => l.LoginID == ID);

        if(oldLogin == null)
        {
            return false;
        }

        oldLogin.Username = newItem.Username;
        oldLogin.Password = newItem.Password;
        
        await _context.SaveChangesAsync();

        return true;
    }
}