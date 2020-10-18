using System.Net;
using MameToppleApi.Controllers;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace MameToppleApi.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> _userServiceMock;
        [SetUp]
        public void Setup()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [Test]
        public void Get_User_Return_UserVM()
        {
            //Arrange 設定初始資料、期望結果//
            //moq 隔離框架
            //模擬出一個UserService物件 此時mock型別為Mock<UserService>
            //moq中可被Mock的類別成員：1.標注public與virtual修飾詞的成員。 2.標注public的界面（interface）所有成員都能被Mock
            //設定(Setup)模擬此UserService物件時，要執行GetById()這個方法，且一定要被執行過才算數(Verifiable可驗證的)
            //註: 此時 mock.Object 物件就是 UserService 型別，可以傳入 UserController 控制器。
            // new UserViewModel屬於fake
            _userServiceMock.Setup(x => x.GetById("test")).Returns(new UserViewModel()
            {
                Account = "test@gmail.com",
                NickName = "test01",
                Avatar = "test",
                Win = 0,
                Lose = 0
            });

            UserController controller = new UserController(_userServiceMock.Object);

            //////////////////////////////

            //Act 呼叫哪個方法進行測試
            var result = controller.GetUser("test").Value;

            //Assert 比較預期結果和實際運行結果，應該要相等
            Assert.IsNotNull(result, "測試失敗：結果為null");
            Assert.IsInstanceOf(typeof(UserViewModel), result, "測試失敗：錯誤的Model");
            // Assert.AreEqual()
        }

        [Test]
        public void User_CreateUser_Return_BadRequset()
        {
            // Arrange
            var userModel = new User();

            UserController controller = new UserController(_userServiceMock.Object);
            //使用 AddModelError 新增錯誤，來測試無效的模型狀態
            controller.ModelState.AddModelError("Account", "Required");

            // Act
            var actual = controller.CreateUser(userModel);

            // Assert
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), actual);
        }

        [Test]
        public void User_CreateUser_Return_OK()
        {
            // Arrange
            var userModel = new User();

            UserController controller = new UserController(_userServiceMock.Object);

            // Act
            var actual = controller.CreateUser(userModel);

            // Assert
            Assert.IsInstanceOf(typeof(OkResult), actual);
        }
    }
}