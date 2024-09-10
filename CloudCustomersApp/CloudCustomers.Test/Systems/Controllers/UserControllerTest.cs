using CloudCustomers.Controllers;
using CloudCustomers.Models;
using CloudCustomers.Services.Users;
using CloudCustomers.Test.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.Test.Systems.Controllers;

public class UserControllerTest
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _userController;

    public UserControllerTest()
    {
        _mockUserService = new Mock<IUserService>();
        _userController = new(_mockUserService.Object);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(UserFixture.GetTestUsers());
        OkObjectResult result = (OkObjectResult)await _userController.GetUsers();

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_Invokes_UserService_ExactlyOnce()
    {
        _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(UserFixture.GetTestUsers());
        OkObjectResult result = (OkObjectResult)await _userController.GetUsers();

        _mockUserService.Verify(service => service.GetUsers(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_Returns_List_Of_Users()
    {
        _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(UserFixture.GetTestUsers());
        OkObjectResult result = (OkObjectResult)await _userController.GetUsers();

        result.Should().BeOfType<OkObjectResult>();
        result.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns_404()
    {
        _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(new List<User>());
        IActionResult result = await _userController.GetUsers();

        result.Should().BeOfType<NotFoundResult>();
        NotFoundResult notFoundResult = (NotFoundResult)result;
        notFoundResult.StatusCode.Should().Be(404);
    }

    [Theory]
    [InlineData("foo", 1)]
    [InlineData("bar", 2)]
    public void Test2(string param, int param2)
    {

    }
}