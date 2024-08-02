using HelpMeCook.API.DAO;
using HelpMeCook.API.Models;
using uUtil = HelpMeCook.API.Utilities.UserUtility;

namespace HelpMeCook.API.Services;

public class UserService : IUserService
{
    private readonly UserRepo _userRepo;
    
    public UserService(UserRepo userRepo)
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

    public async Task<bool> UpdateUser(UserDTO userToUpdate)
    {
        User user = uUtil.DTOToUser(userToUpdate);
        return await _userRepo.Update(user);
    }

    public async Task<User> DeleteUser(UserDTO userToDelete)
    {
        User user = uUtil.DTOToUser(userToDelete);
        return await _userRepo.Delete(user);
    }
}