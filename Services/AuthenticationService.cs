namespace backend_dev_project.Services;

public record UserInfo(string id, string username, string email);
public record AuthenticationRequestBody(string username, string password);
public record AuthenticationResponseBody(string token);

public interface IAuthenticationService {
    Task<UserInfo> ValidateUser(string username, string password);

    Task<User> AddUser(User user);
}

public class AuthenticationService : IAuthenticationService {

    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<UserInfo> ValidateUser(string username, string password) {
        var user = await _userRepository.GetUser(username);
        if (user == null) {
            return null;
        }
        bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (passwordMatch) {
            return new UserInfo(user.Id, user.Username, user.Email);
        }
        return null;
        // Check user password valid
        
    }

    public async Task<User> AddUser(User user){
        var exists = await _userRepository.GetUser(user.Username);

        if (exists != null) {
            return null;
        }

        var hash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = hash;

        await _userRepository.AddUser(user);
        return await _userRepository.GetUser(user.Username);
    }

    
}