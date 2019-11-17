using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using RetailerAPI.Models;

namespace RetailerAPI.Controllers.Tests
{
    public class UserControllerTest
    {
        [Test]
        public void TestGetUserToken()
        {
            // Expected result
            UserToken userToken = new UserToken
            {
                Name = "random name",
                Token = "random token"
            };

            // Set up behavior
            Mock<IOptions<UserToken>> mockUserTokenOption = new Mock<IOptions<UserToken>>();
            mockUserTokenOption.Setup(t => t.Value).Returns(userToken);
            UserController userController = new UserController(mockUserTokenOption.Object);

            // Call method and assert
            Assert.AreEqual(userToken, userController.Get().Value);
        }
    }
}