using HelpMeCook.API.DAO;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Exceptions;
using HelpMeCook.API.Models;
using uUtil = HelpMeCook.API.Utilities.UserUtility;

namespace HelpMeCook.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;
    
    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<User> CreateUser(UserDTO userToCreate)
    {
        User newUser = uUtil.DTOToUser(userToCreate);
        return await _userRepo.Create(newUser);
    }

    public async Task<ICollection<User>> GetAllUsers()
    {
        return await _userRepo.GetAll()!;
    }

    public async Task<User?> GetUserByID(int userID)
    {
        if(userID < 1) throw new ArgumentException("Invalid ID");
        return await _userRepo.GetByID(userID);
    }

    public async Task<bool> UpdateUser(int ID, UserDTO userToUpdate)
    {
        User user = uUtil.DTOToUser(userToUpdate);
        return await _userRepo.Update(ID, user);
    }

    public async Task<User?> DeleteUser(int ID)
    {
        User? userByID = await _userRepo.GetByID(ID);

        if(userByID == null)
        {
            throw new InvalidUserException("User does not exst.");
        }
        
        return await _userRepo.Delete(ID);
    }
}