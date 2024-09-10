using CloudCustomers.Models;

namespace CloudCustomers.Test.Fixtures;

public static class UserFixture
{
    public static List<User> GetTestUsers()
    {
        return new List<User>() {
            new User() { Id = 1, Name =  "alex", Email = "alex@test.fr", Address = new Address() { City = "Lyon", Street = "victor hugo", ZipCode = "69200" } },
            new User() { Id = 2, Name =  "seb", Email = "seb@test.fr", Address = new Address() { City = "Paris", Street = "emile zola", ZipCode = "75600" } },
            new User() { Id = 3, Name =  "kate", Email = "kate@test.fr", Address = new Address() { City = "Marseille", Street = "de gaulle", ZipCode = "23000" } },
        };
    }
}
