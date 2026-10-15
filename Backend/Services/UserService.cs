using Backend.Models.Entities;

namespace Backend.Services;

public interface IUserService
{
    User? Login(string Email, string PasswordHash);
    void Logout();
    User CreateUser(User userData);
    void DeactivateUser(Guid Id);
    User? GetUserById(Guid Id);
    List<User> GetActiveUsers();
}

public class UserService : IUserService
{
    public User? Login(string Email, string PasswordHash)
    {
        Console.WriteLine("UserService: Login request received");
        return null; // TODO: implement
    }

    public void Logout()
    {
        Console.WriteLine("UserService: Logout request received");
    }

    public User CreateUser(User userData)
    {
        Console.WriteLine("UserService: CreateUser request received");
        return userData; // TODO: implement
    }

    public void DeactivateUser(Guid Id)
    {
        Console.WriteLine("UserService: DeactivateUser request received");
    }

    public User? GetUserById(Guid Id)
    {
        Console.WriteLine("UserService: GetUserById request received");
        return null; // TODO: implement
    }

    public List<User> GetActiveUsers()
    {
        Console.WriteLine("UserService: GetActiveUsers request received");
        return new List<User>(); // TODO: implement
    }
}