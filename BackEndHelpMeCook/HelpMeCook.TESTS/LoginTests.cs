using Moq;
using HelpMeCook.API.DAO.Interfaces;
using HelpMeCook.API.Services;
using HelpMeCook.API.Models;

namespace HelpMeCook.Tests
{
    public class LoginTests
    {
        [Theory]
        [InlineData("username", "password")]
        public async void TestGetLoginByUsernameAndPassword(string username, string password)
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            loginRepoMock.Setup(repo => repo.GetByUsernameAndPassword(username, password)).Returns(Task.FromResult(new Login { Username = username, Password = password })!);

            Login? login = await loginService.GetByUsernameAndPassword("username", "password");
            Assert.Equal("username", login?.Username);
            Assert.Equal("password", login?.Password);
        }

        [Fact]
        public async void TestGetAllLogins()
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            ICollection<Login> logins = new List<Login>
            {
                new Login { Username = "user1", Password = "pass1" },
                new Login { Username = "user2", Password = "pass2" },
                new Login { Username = "user3", Password = "pass3" }
            };

            loginRepoMock.Setup(repo => repo.GetAll()).Returns(Task.FromResult(logins));

            ICollection<Login> result = await loginService.GetAllLogins();
            Assert.Equal(logins, result);
        }
        [Fact]
        public async void TestCreateLogin()
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
        public async void TestUpdateLogin()
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
        public async void TestDeleteLogin()
        {
            Mock<ILoginRepo> loginRepoMock = new();
            LoginService loginService = new(loginRepoMock.Object);

            Login login = new Login { Username = "username", Password = "password" };

            loginRepoMock.Setup(repo => repo.GetByID(1)).Returns(Task.FromResult(login)!);
            loginRepoMock.Setup(repo => repo.Delete(1)).Returns(Task.FromResult(login)!);

            Login? result = await loginService.DeleteLogin(1);
        }
    }
}