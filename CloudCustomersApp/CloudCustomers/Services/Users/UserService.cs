using CloudCustomers.Configs;
using CloudCustomers.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace CloudCustomers.Services.Users;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly UsersApiOptions _apiConfig;

    public UserService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
    }

    public async Task<List<User>> GetUsers()
    {
        HttpResponseMessage userResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);

        if (userResponse.StatusCode == HttpStatusCode.NotFound) return new List<User>();

        HttpContent responseContent = userResponse.Content;
        List<User>? users = await responseContent.ReadFromJsonAsync<List<User>>();
        return users ?? new List<User>();
    }
}
