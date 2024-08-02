using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelpMeCook.API.Models;
using HelpMeCook.API.DAO;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Utilities;

namespace HelpMeCook.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _loginRepo;

        public LoginService (ILoginRepo loginRepo)
        {
            this._loginRepo = loginRepo;
        }

        public async Task<Login> CreateLogin(LoginDTO newLogin)
        {
            Login log = LoginUtility.DTOToLogin(newLogin);
            return await _loginRepo.Create(log);
           
        }

        public Task<Login?> DeleteLogin(LoginDTO loginToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Login>> GetAllLogins()
        {
            throw new NotImplementedException();
        }

        public Task<Login?> GetLoginByID(int loginID)
        {
            throw new NotImplementedException();
        }

        public Task<Login?> UpdateLogin(LoginDTO newLogin)
        {
            throw new NotImplementedException();
        }
    }
}
