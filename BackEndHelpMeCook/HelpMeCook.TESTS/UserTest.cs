using Moq;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Models;
using HelpMeCook.API.Services;
using HelpMeCook.API.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace HelpMeCook.Tests
{
    public class UserTest
    {
        private readonly Mock<IUserRepo> _userRepoMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<SignInManager<User>> _signInManagerMock;
        private readonly UserService _userService;

        public UserTest()
        {
            _userRepoMock = new Mock<IUserRepo>();

            var userStoreMock = new Mock<IUserStore<User>>();
            _userManagerMock = new Mock<UserManager<User>>(userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();
            _signInManagerMock = new Mock<SignInManager<User>>(_userManagerMock.Object, contextAccessorMock.Object, userPrincipalFactoryMock.Object, null!, null!, null!, null!);

            _userService = new UserService(_userRepoMock.Object, _signInManagerMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnUser()
        {

            var userDTO = new UserDTO
            {
                FirstName = "John",
                LastName = "Doe"
            };
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe"
            };

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.CreateUser(userDTO);

            // Assert
            Assert.True(result.Succeeded);

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


            // Assert
            Assert.Equal(users.Count, result.Count);
            _userRepoMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetUserByID_ShouldReturnUser()
        {

            string userID = "1";
            var user = new User { Id = "1", UserName = "Doe" };

            _userRepoMock.Setup(repo => repo.GetByID(userID)).ReturnsAsync(user);


            var result = await _userService.GetUserByID(userID);


            Assert.NotNull(result);
            Assert.Equal(user, result);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            _userRepoMock.Verify(repo => repo.GetByID(userID), Times.Once);
        }

        [Fact]
        public async Task GetUserByID_InvalidID_ShouldThrowArgumentException()
        {

            await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetUserByID("0"));
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnTrue()
        {

            string userID = "1";
            var userDTO = new UserDTO { FirstName = "John", LastName = "Doe" };
            var user = new User { FirstName = "Johnn", LastName = "Doe" };

            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);


            var result = await _userService.UpdateUser(userID, userDTO);


            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.UpdateUser("1", new UserDTO());

            // Assert
            Assert.False(result);
        }


        [Fact]
        public async Task DeleteUser_ShouldReturnUser()
        {

            string userID = "1";
            var user = new User { FirstName = "Johnn", LastName = "Doe" };

            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);


            var result = await _userService.DeleteUser(userID);

            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
        }

        [Fact]
        public async Task DeleteUser_UserNotFound_ShouldThrowInvalidUserException()
        {

            string userID = "1";

            _userRepoMock.Setup(repo => repo.GetByID(userID)).ReturnsAsync((User?)null);


            await Assert.ThrowsAsync<InvalidUserException>(() => _userService.DeleteUser(userID));
        }

        [Fact]
        public async Task LoginUser_ShouldReturnSignInResult()
        {
            // Arrange
            var userDto = new UserDTO { Username = "username", Password = "Password" };

            _signInManagerMock.Setup(s => s.PasswordSignInAsync(userDto.Username, userDto.Password, false, false)).ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _userService.LoginUser(userDto);

            // Assert
            Assert.Equal(SignInResult.Success, result);
        }

        [Fact]
        public async Task LoginUser_ShouldCallSignOut()
        {
            
            // Act
            await _userService.LogoutUser();

            // Assert
            _signInManagerMock.Verify(s => s.SignOutAsync(), Times.Once);
        }
    }
}