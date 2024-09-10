using CloudCustomers.Models;
using CloudCustomers.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        List<User> users = await _userService.GetUsers();

        if (users is null || users.Count == 0) return NotFound();

        return Ok(users);
    }
}
