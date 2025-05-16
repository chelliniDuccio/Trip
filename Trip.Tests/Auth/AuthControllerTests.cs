using Microsoft.AspNetCore.Mvc;
using Moq;
using Trip.Controllers;
using Trip.Models;
using Trip.Models.Extra;
using Trip.Services.Interfaces;
using Trip.Services.Interfaces.Extra;

namespace Trip.Tests.Auth
{
    public class AuthControllerTests
    {
        private readonly Mock<IJwtTokenService> _mockJwtTokenService;
        private readonly Mock<IUserService> _mockUserService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockJwtTokenService = new Mock<IJwtTokenService>();
            _mockUserService = new Mock<IUserService>();
            _controller = new AuthController(_mockJwtTokenService.Object, _mockUserService.Object);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_IfRequestIsNull()
        {
            // Act
            var result = await _controller.Login(null);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var message = badRequest.Value.GetType().GetProperty("message")?.GetValue(badRequest.Value, null);
            Assert.Equal("Email and password are required.", message);
        }

        [Theory]
        [InlineData(null, "password")]
        [InlineData("user", null)]
        [InlineData("", "password")]
        [InlineData("user", "")]
        [InlineData(" ", "password")]
        [InlineData("user", " ")]
        public async Task Login_ReturnsBadRequest_IfUserOrPasswordMissing(string user, string password)
        {
            // Arrange
            var request = new LoginRequest { User = user, Password = password };

            // Act
            var result = await _controller.Login(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            var message = badRequest.Value.GetType().GetProperty("message")?.GetValue(badRequest.Value, null);
            Assert.Equal("Email and password are required.", message);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_IfUserNotFound()
        {
            // Arrange
            var request = new LoginRequest { User = "user", Password = "pass" };
            _mockUserService.Setup(s => s.GetUserByUsernameOrEmailAsync("user")).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
            var message = unauthorized.Value.GetType().GetProperty("message")?.GetValue(unauthorized.Value, null);
            Assert.Equal("Invalid email.", message);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_IfPasswordInvalid()
        {
            // Arrange
            var request = new LoginRequest { User = "user", Password = "wrongpass" };
            var user = new User
            {
                Id = 1,
                Username = "user",
                Email = "user@email.com",
                Name = "Name",
                Surname = "Surname",
                Password = "hashedpass"
            };
            _mockUserService.Setup(s => s.GetUserByUsernameOrEmailAsync("user")).ReturnsAsync(user);
            _mockUserService.Setup(s => s.HashPassword("wrongpass")).Returns("notmatching");

            // Act
            var result = await _controller.Login(request);

            // Assert
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
            var message = unauthorized.Value.GetType().GetProperty("message")?.GetValue(unauthorized.Value, null);
            Assert.Equal("Invalid password.", message);
        }

        [Fact]
        public async Task Login_ReturnsOk_WithTokenAndUserInfo_IfCredentialsValid()
        {
            // Arrange
            var request = new LoginRequest { User = "user", Password = "pass" };
            var user = new User
            {
                Id = 1,
                Username = "user",
                Email = "user@email.com",
                Name = "Name",
                Surname = "Surname",
                Password = "hashedpass",
            };
            _mockUserService.Setup(s => s.GetUserByUsernameOrEmailAsync("user")).ReturnsAsync(user);
            _mockUserService.Setup(s => s.HashPassword("pass")).Returns("hashedpass");
            _mockJwtTokenService.Setup(s => s.GenerateToken(user)).Returns("token123");

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            dynamic value = okResult.Value;
            Assert.Equal("token123", value.GetType().GetProperty("token")?.GetValue(value, null));

            // Get the 'user' property from the outer anonymous object
            var userObj = value.GetType().GetProperty("user")?.GetValue(value, null);

            // Now use reflection to access properties of the inner 'user' object
            Assert.Equal(1, (int)userObj.GetType().GetProperty("id")?.GetValue(userObj, null));
            Assert.Equal("user", (string)userObj.GetType().GetProperty("username")?.GetValue(userObj, null));
            Assert.Equal("user@email.com", (string)userObj.GetType().GetProperty("email")?.GetValue(userObj, null));
            Assert.Equal("Name", (string)userObj.GetType().GetProperty("name")?.GetValue(userObj, null));
            Assert.Equal("Surname", (string)userObj.GetType().GetProperty("surname")?.GetValue(userObj, null));
        }
    }
}
