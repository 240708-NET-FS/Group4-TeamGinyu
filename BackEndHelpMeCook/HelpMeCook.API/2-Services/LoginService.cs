using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelpMeCook.API.Models;
using HelpMeCook.API.DAO;

namespace HelpMeCook.API.Services
{
    public class LoginService
    {
        private readonly AppDbContext _context;

        public LoginService(AppDbContext context)
        {
            _context = context;
        }

     
        public async Task<Login?> GetLoginByUsernameAsync(string username)
        {
            return await _context.Login
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.Username == username);
        }

      
        public async Task<Login> CreateLoginAsync(Login login)
        {
        
            _context.Login.Add(login);
            await _context.SaveChangesAsync();
            return login;
        }

       
        public async Task<Login?> UpdateLoginAsync(int loginId, Login updatedLogin)
        {
            var login = await _context.Login.FindAsync(loginId);

            if (login == null)
            {
                return null;
            }

            login.Username = updatedLogin.Username;
            login.Password = updatedLogin.Password;
            login.UserID = updatedLogin.UserID;

            _context.Login.Update(login);
            await _context.SaveChangesAsync();

            return login;
        }

        public async Task<bool> DeleteLoginAsync(int loginId)
        {
            var login = await _context.Login.FindAsync(loginId);

            if (login == null)
            {
                return false;
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
