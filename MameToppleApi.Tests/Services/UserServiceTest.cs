using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using Moq;
using NUnit.Framework;

namespace MameToppleApi.Tests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IRepository<User>> _genericRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _genericRepositoryMock = new Mock<IRepository<User>>();
        }

        [Test]
        public void User_GetUser_Return_UserVM()
        {
            //Arrange 設定初始資料、期望結果




            //Act 呼叫哪個方法進行測試

            //Assert 比較預期結果和實際運行結果，應該要相等

        }

    }
}