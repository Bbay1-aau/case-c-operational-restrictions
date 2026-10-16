using Backend.Models.Entities;

namespace Backend.Services;

//TODO: This interface should be seperated into its own file
public interface IUserService
{
    LoginResponseDto Login(string Email, string PasswordHash);
    bool Logout();
    UserDto CreateUser(CreateUserDto userData);
    User DeactivateUser(Guid userId);
    List<User> GetActiveUsers();
}

public class UserService : IUserService
{
    public LoginResponseDto Login(string Email, string PasswordHash)
    {
        Console.WriteLine("UserService: Login request received");
        return null; // TODO: implement
    }

    public bool Logout()
    {
        Console.WriteLine("UserService: Logout request received");
    }

    public UserDto CreateUser(CreateUserDto userData)
    {
        Console.WriteLine("UserService: CreateUser request received");
        return userData; // TODO: implement
    }

    public User DeactivateUser(Guid userId)
    {
        Console.WriteLine("UserService: DeactivateUser request received");
    }

    public List<User> GetActiveUsers()
    {
        Console.WriteLine("UserService: GetActiveUsers request received");
        return new List<User>(); // TODO: implement
    }
}