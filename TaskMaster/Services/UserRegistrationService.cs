using UserModel;

namespace Services;

public enum UserRegistrationStatus
{
    Success,
    UsernameAlreadyExists,
    SaveFailed
}

public sealed record UserRegistrationResult(
    UserRegistrationStatus Status,
    User? User
);

public sealed class UserRegistrationService
{
    private readonly IUserRepository _userRepository;

    public UserRegistrationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserRegistrationResult Register(
        Role role,
        string name,
        string lastName,
        string username,
        string password)
    {
        if (_userRepository.ExistsByUsername(username))
        {
            return new UserRegistrationResult(
                UserRegistrationStatus.UsernameAlreadyExists,
                null
            );
        }

        User user = new(
            role,
            name,
            lastName,
            username,
            password,
            Level.Beginner,
            0
        );

        if (!_userRepository.Add(user))
        {
            return new UserRegistrationResult(
                UserRegistrationStatus.SaveFailed,
                null
            );
        }

        return new UserRegistrationResult(
            UserRegistrationStatus.Success,
            user
        );
    }
}
