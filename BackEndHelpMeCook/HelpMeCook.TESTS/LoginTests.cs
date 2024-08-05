using Moq;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Services;
using HelpMeCook.API.Models;
using HelpMeCook.API.Exceptions;

namespace HelpMeCook.Tests
{
    public class LoginTests
    {
        [Theory]
        [InlineData("username", "password")]
        public async void GetLoginByUsernameAndPassword_ShouldReturnLogin(string username, string password)
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            loginRepoMock.Setup(repo => repo.GetByUsernameAndPassword(username, password)).Returns(Task.FromResult(new Login { Username = username, Password = password })!);

            Login? login = await loginService.GetByUsernameAndPassword("username", "password");
            Assert.Equal("username", login?.Username);
            Assert.Equal("password", login?.Password);
        }

        [Theory]
        [InlineData("", "")]
        public async void GetLoginByUsernameAndPassword_ShouldThrowException(string username, string password)
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            // loginRepoMock.Setup(repo => repo.GetByUsernameAndPassword(username, password)).Returns(Task.FromResult(new Login { Username = username, Password = password })!);

            // Login? login = await loginService.GetByUsernameAndPassword(username, password);
            await Assert.ThrowsAsync<InvalidLoginException>(async () => await loginService.GetByUsernameAndPassword(username, password));
        }

        [Theory]
        [InlineData("username")]
        public async void GetByUsername_ShouldReturnLogin(string username)
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            loginRepoMock.Setup(repo => repo.GetByUsername(username)).Returns(Task.FromResult(new Login { Username = username })!);

            Login? login = await loginService.GetByUsername("username");
            Assert.Equal("username", login?.Username);
        }

        [Theory]
        [InlineData("")]
        public async void GetByUsername_ShouldThrowException(string username)
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            //loginRepoMock.Setup(repo => repo.GetByUsername(username)).Returns(Task.FromResult(new Login { Username = username })!);

            // Login? login = await loginService.GetByUsername(username);
            
            await Assert.ThrowsAsync<InvalidLoginException>(async () => await loginService.GetByUsername(username)); 
        }

        [Theory]
        [InlineData(1)]
        public async void GetByID_ShouldReturnLogin(int id)
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            loginRepoMock.Setup(repo => repo.GetByID(id)).Returns(Task.FromResult(new Login { UserID = id })!);

            Login? login = await loginService.GetLoginByID(1);
            Assert.Equal(1, login?.UserID);
        }
        [Theory]
        [InlineData(0)]
        public async void GetByID_ShouldThrowException(int id)
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            // loginRepoMock.Setup(repo => repo.GetByID(id)).Returns(Task.FromResult(new Login { UserID = id })!);

            // Login? login = await loginService.GetLoginByID(0);

            await Assert.ThrowsAsync<ArgumentException>(async () => await loginService.GetLoginByID(id));
        }

        [Fact]
        public async void GetAllLogins_ShouldReturnAllLogins()
        {
            ICollection<Login> logins = new List<Login>
            {
                new Login { Username = "user1", Password = "pass1" },
                new Login { Username = "user2", Password = "pass2" },
                new Login { Username = "user3", Password = "pass3" }
            };

            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            loginRepoMock.Setup(repo => repo.GetAll()).Returns(Task.FromResult(logins));

            ICollection<Login> result = await loginService.GetAllLogins();
            Assert.Equal(logins, result);
        }

        [Fact]
        public async void CreateLogin_ShouldReturnNewLogin()
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            Login login = new Login { Username = "username", Password = "password" };

            loginRepoMock.Setup(repo => repo.Create(It.IsAny<Login>())).ReturnsAsync(login);

            Login result = await loginService.CreateLogin(new LoginDTO { Username = "username", Password = "password" });

            Assert.NotNull(result); // Add this check to ensure result is not null
            Assert.Equal("username", result.Username);
            Assert.Equal("password", result.Password);
        }
        
        [Fact]
        public async void CreateLogin_ShouldThrowException()
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            Login result = await loginService.CreateLogin(new LoginDTO { Username = "", Password = "" });

            await Assert.ThrowsAsync<InvalidLoginException>(async () => await loginService.CreateLogin(new LoginDTO { Username = "", Password = "" }));
        }
        
        [Fact]
        public async void UpdateLogin_ShouldUpdateLogin()
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            Login login = new Login { LoginID = 1, UserID = 1, Username = "username", Password = "password" };
            LoginDTO loginDto = new LoginDTO { UserID = 1, Username = "username", Password = "password" };
         

            loginRepoMock.Setup(repo => repo.Create(It.IsAny<Login>())).ReturnsAsync(login);
            loginRepoMock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Login>())).Returns(Task.FromResult(true)!);

            Login loginCreated = await loginService.CreateLogin(loginDto);

            bool updated = await loginService.UpdateLogin(loginCreated.LoginID, new LoginDTO {UserID = 1, Username = "newUsername", Password = "newPassword" });
            
            Console.WriteLine(updated);

            Assert.True(updated);
        }

        [Fact]
        public async void DeleteLogin_ShouldThrowException()
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            Login login = new Login { Username = "username", Password = "password" };

            await Assert.ThrowsAsync<InvalidLoginException>(async () => await loginService.DeleteLogin(0));
        }

        [Fact]
        public async void DeleteLogin_ShouldDeleteLogin()
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            Login login = new Login { Username = "username", Password = "password" };

            loginRepoMock.Setup(repo => repo.GetByID(1)).Returns(Task.FromResult(login)!);
            loginRepoMock.Setup(repo => repo.Delete(1)).Returns(Task.FromResult(login)!);

            Login? result = await loginService.DeleteLogin(1);
            await Assert.ThrowsAsync<InvalidLoginException>(async () => await loginService.DeleteLogin(0));
        }
    }
}