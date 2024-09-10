using CloudCustomers.Models;

namespace CloudCustomers.Services.Users;

public interface IUserService
{
    Task<List<User>> GetUsers();
}
