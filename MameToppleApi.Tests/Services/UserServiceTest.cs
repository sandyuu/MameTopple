using System;
using System.Linq.Expressions;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using MameToppleApi.Service;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace MameToppleApi.Tests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        //Fake資料
        private List<User> fakeUserList = new List<User>()
        {
            new User{Account="t1",Password="t1",Avatar="t1",NickName="t1",Win=0,Lose=0},
            new User{Account="t2",Password="t2",Avatar="t2",NickName="t2",Win=0,Lose=0},
            new User{Account="t3",Password="t3",Avatar="t3",NickName="t3",Win=0,Lose=0}
        };

        private Mock<IRepository<User>> _genericRepositoryMock;
        private Mock<IEncryptionAdapter> _argon2AdapterMock;

        [SetUp]
        public void Setup()
        {
            _genericRepositoryMock = new Mock<IRepository<User>>();
            _argon2AdapterMock = new Mock<IEncryptionAdapter>();
        }

        [Test]
        public void When_GiveAccount_Then_GetUserVM()
        {
            //Arrange 設定初始資料、期望結果
            Expression<Func<User, bool>> fakeEx = x => x.Account == "Test@gmail.com";
            User fakeUser = new User
            {
                Account = "admin@gmial.com",
                Password = "123456",
                Avatar = "Mame",
                NickName = "Admin01",
                Win = 0,
                Lose = 0
            };
            _genericRepositoryMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<User, bool>>>())).Returns(fakeUser);
            UserService service = new UserService(_genericRepositoryMock.Object, _argon2AdapterMock.Object);

            //Act 呼叫哪個方法進行測試
            var actual = service.GetById("admin@gmail.com");
            //Assert 比較預期結果和實際運行結果，應該要相等
            Assert.IsInstanceOf(typeof(UserViewModel), actual);
        }

        [Test]
        public void When_LoginModel_UserNotExist_Then_ReturnFalse()
        {
            //Arrange 安排        
            LoginViewModel fakeLoginVM = new LoginViewModel
            {
                Account = "t4",
                Password = "t4"
            };
            _genericRepositoryMock.Setup(X => X.GetAllAsync()).ReturnsAsync(fakeUserList);
            UserService userService = new UserService(_genericRepositoryMock.Object, _argon2AdapterMock.Object);
            //Act 執行
            var actual = userService.LoginVerify(fakeLoginVM).Result;
            //Assert 斷言
            Assert.AreEqual(actual, false);
        }

        [Test]
        public void When_LoginModel_UserIsExist_Then_ReturnTrue()
        {
            //Arrange 安排
            LoginViewModel fakeLoginVM = new LoginViewModel
            {
                Account = "t1",
                Password = "t1"
            };
            _genericRepositoryMock.Setup(X => X.GetAllAsync()).ReturnsAsync(fakeUserList);
            _argon2AdapterMock.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            UserService userService = new UserService(_genericRepositoryMock.Object, _argon2AdapterMock.Object);
            //Act 執行
            var actual = userService.LoginVerify(fakeLoginVM).Result;
            //Assert 斷言
            Assert.AreEqual(actual, true);
        }
    }
}