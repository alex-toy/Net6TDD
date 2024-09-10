using CloudCustomers.Configs;
using CloudCustomers.Models;
using CloudCustomers.Services.Users;
using CloudCustomers.Test.Fixtures;
using CloudCustomers.Test.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomers.Test.Systems.Services;

public class UserServiceTest
{
    [Fact]
    public async Task GetUsers_Invokes_HttpGetRequest()
    {
        string endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions { Endpoint = endpoint });
        var expectedResponse = UserFixture.GetTestUsers();
        var mockHttpMessageHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var userService = new UserService(httpClient, config);

        _ = await userService.GetUsers();

        mockHttpMessageHandler
            .Protected()
            .Verify(
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
             );
    }

    [Fact]
    public async Task GetUsers_When404_Returns_Empty_ListOfUsers()
    {
        string endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions { Endpoint = endpoint });
        var mockHttpMessageHandler = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var userService = new UserService(httpClient, config);

        List<User> result = await userService.GetUsers();

        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetUsers_Returns_ListOfUsers_Of_Expected_Size()
    {
        string endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions { Endpoint = endpoint });
        var expectedResponse = UserFixture.GetTestUsers();
        var mockHttpMessageHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var userService = new UserService(httpClient, config);

        List<User> result = await userService.GetUsers();

        result.Count.Should().Be(expectedResponse.Count);
    }

    [Fact]
    public async Task GetUsers_Invokes_Configured_Url()
    {
        List<User> expectedResponse = UserFixture.GetTestUsers();
        string endpoint = "https://example.com/users";
        IOptions<UsersApiOptions> config = Options.Create(new UsersApiOptions { Endpoint = endpoint });
        Mock<HttpMessageHandler> mockHttpMessageHandler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
        HttpClient httpClient = new HttpClient(mockHttpMessageHandler.Object);

        UserService userService = new (httpClient, config);

        List<User> result = await userService.GetUsers();

        mockHttpMessageHandler
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == new Uri(endpoint)),
                ItExpr.IsAny<CancellationToken>()
             );
    }
}
