using Moq;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using HelpMeCook.API.Exceptions;

namespace HelpMeCook.Tests
{
public class UserTest
{
    private readonly Mock<IUserRepo> _userRepoMock;
    private readonly UserService _userService;

    public UserTest()
    {
        _userRepoMock = new Mock<IUserRepo>();
        _userService = new UserService(_userRepoMock.Object);
    }

    [Fact]
    public async Task CreateUser_ShouldReturnUser()
    {
   
        var userDTO = new UserDTO 
        { 
           FirstName = "John", LastName = "Doe"
        };
        var user = new User
        { 
           FirstName = "John", LastName = "Doe" 
        };

        _userRepoMock.Setup(repo => repo.Create(It.IsAny<User>())).ReturnsAsync(user);

        
        var result = await _userService.CreateUser(userDTO);

  
        Assert.NotNull(result);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName,result.LastName);
        _userRepoMock.Verify(repo => repo.Create(It.IsAny<User>()), Times.Once);
        
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnCollectionOfUsers()
    {
       
        var users = new List<User>
        {
            new User {FirstName = "John", LastName= "Doe"},
            new User {FirstName = "Siya" ,LastName = "Smith"}
        };

        _userRepoMock.Setup(repo => repo.GetAll()!).ReturnsAsync(users);

      
        var result = await _userService.GetAllUsers();

    
        Assert.NotNull(result);
        Assert.Equal(users.Count, result.Count);
        Assert.Contains(result, u => u.FirstName =="John" && u.LastName == "Doe");
        Assert.Contains(result, u => u.FirstName =="Siya" && u.LastName == "Smith");
        _userRepoMock.Verify(repo => repo.GetAll(), Times.Once);
    }

    [Fact]
    public async Task GetUserByID_ShouldReturnUser()
    {
        
        int userID = 1;
        var user = new User { FirstName = "John" ,LastName = "Doe" };

        _userRepoMock.Setup(repo => repo.GetByID(userID)).ReturnsAsync(user);

      
        var result = await _userService.GetUserByID(userID);

     
        Assert.NotNull(result);
        Assert.Equal(user, result);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName,result.LastName);
        _userRepoMock.Verify(repo => repo.GetByID(userID), Times.Once);
    }

    [Fact]
    public async Task GetUserByID_InvalidID_ShouldThrowArgumentException()
    {
      
        await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetUserByID(0));
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnTrue()
    {
     
        int userID = 1;
        var userDTO = new UserDTO { FirstName = "John" , LastName = "Doe" };
        var user = new User { FirstName = "Johnn" ,LastName = "Doe" };

        _userRepoMock.Setup(repo => repo.Update(userID, It.IsAny<User>())).ReturnsAsync(true);

    
        var result = await _userService.UpdateUser(userID, userDTO);

    
        Assert.True(result);
        _userRepoMock.Verify(repo => repo.Update(userID, It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnUser()
    {
    
        int userID = 1;
        var user = new User { FirstName = "Johnn" ,LastName = "Doe" };

        _userRepoMock.Setup(repo => repo.GetByID(userID)).ReturnsAsync(user);
        _userRepoMock.Setup(repo => repo.Delete(userID)).ReturnsAsync(user);

        var result = await _userService.DeleteUser(userID);

        Assert.NotNull(result);
        Assert.Equal(user.FirstName, result.FirstName);
        Assert.Equal(user.LastName,result.LastName);
        _userRepoMock.Verify(repo => repo.GetByID(userID), Times.Once);
        _userRepoMock.Verify(repo => repo.Delete(userID), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_UserNotFound_ShouldThrowInvalidUserException()
    {
     
        int userID = 1;

        _userRepoMock.Setup(repo => repo.GetByID(userID)).ReturnsAsync((User?)null);

      
        await Assert.ThrowsAsync<InvalidUserException>(() => _userService.DeleteUser(userID));
    }
}
}