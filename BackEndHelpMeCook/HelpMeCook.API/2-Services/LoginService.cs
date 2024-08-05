using HelpMeCook.API.Models;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Utilities;
using Microsoft.IdentityModel.Tokens;

namespace HelpMeCook.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _loginRepo;

        public LoginService(ILoginRepo loginRepo)
        {
            this._loginRepo = loginRepo;
        }

        public async Task<Login> CreateLogin(LoginDTO newLogin)
        {
            if (newLogin.Username.IsNullOrEmpty() || newLogin.Password.IsNullOrEmpty())
            {
                throw new InvalidLoginException("Username and password must be provided.");

            }
            Login log = LoginUtility.DTOToLogin(newLogin);

            return await _loginRepo.Create(log);
        }

        public async Task<Login?> DeleteLogin(int ID)
        {
            Login? login = await _loginRepo.GetByID(ID);

            if (login == null)
            {
                throw new InvalidLoginException("User does not exst.");
            }

            return await _loginRepo.Delete(ID);
        }

        public Task<ICollection<Login>> GetAllLogins()
        {
            return _loginRepo.GetAll();
        }

        public async Task<Login?> GetByUsername(string username)
        {
            if (username.IsNullOrEmpty())
            {
                throw new InvalidLoginException("Wrong username and/or password provided.");
            }

            Login? login = await _loginRepo.GetByUsername(username);

            if (login == null)
            {
                throw new InvalidLoginException("Wrong username and/or password provided.");
            }

            return login;
        }

        public async Task<Login?> GetByUsernameAndPassword(string username, string password)
        {
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                throw new InvalidLoginException("Wrong username and/or password provided.");
            }

            Login? login = await _loginRepo.GetByUsernameAndPassword(username, password);

            if (login == null)
            {
                throw new InvalidLoginException("Wrong username and/or password provided.");
            }

            return login;
        }

        public async Task<Login?> GetLoginByID(int loginID)
        {
            if (loginID < 1) throw new ArgumentException("Invalid ID");

            Login? log = await _loginRepo.GetByID(loginID);

            if (log == null)
            {
                throw new InvalidLoginException($"Could not find Login by ID {loginID}");
            }

            return log;
        }

        public async Task<bool> UpdateLogin(int ID, LoginDTO newLogin)
        {
            Login log = LoginUtility.DTOToLogin(newLogin);

            return await _loginRepo.Update(ID, log);
        }
    }
}
