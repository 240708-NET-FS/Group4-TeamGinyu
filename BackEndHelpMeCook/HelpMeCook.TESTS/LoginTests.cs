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
    }
}