using System.Threading.Tasks;
using Castle.Core.Configuration;
using MameToppleApi.Controllers;
using MameToppleApi.Helpers;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using Moq;
using NUnit.Framework;

namespace MameToppleApi.Tests.Controllers
{
    [TestFixture]
    public class TokenControllerTests
    {
        private const bool V = true;
        private Mock<IUserService> _userServiceMock;
        private Mock<IJwtHelpersService> _jwtServiceMock;
        private Mock<ToppleDBContext> _contextMock;
        private Mock<IConfiguration> _configurationMock;

        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
            _configurationMock = new Mock<IConfiguration>();
            _jwtServiceMock = new Mock<IJwtHelpersService>();
            _contextMock = new Mock<ToppleDBContext>();
        }

        [Test]
        public async Task When_Login_Then_Return_BearerTokenAsync()
        {
            // Arrange: 初始化的過程
            var FakeModel = new LoginViewModel()
            {
                Account = "test@gmail.com",
                Password = "123456"
            };
            _jwtServiceMock.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<int>())).Returns("BearerToken");
            _userServiceMock.Setup(x => x.LoginVerify(It.IsAny<LoginViewModel>())).Returns(Task.FromResult(true));
            TokenController controller = new TokenController(_jwtServiceMock.Object, _contextMock.Object, _userServiceMock.Object);
            // Act: 執行測試的目標，並取得實際結果
            var actual = await controller.Login(FakeModel);

            // Assert: 驗證結果
            Assert.IsInstanceOf(typeof(string), actual.Value);
        }
    }
}