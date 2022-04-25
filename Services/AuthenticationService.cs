namespace backend_dev_project.Services;

public record UserInfo(string id, string username, string email);
public record AuthenticationRequestBody(string username, string password);

public interface IAuthenticationService {
    Task<UserInfo> ValidateUser(string username, string password);
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
}