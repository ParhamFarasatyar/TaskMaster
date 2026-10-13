using TaskMaster.Application.Abstractions.Persistence;
using TaskMaster.Domain;

namespace TaskMaster.Application.Services;

public sealed class AuthenticationService
{
    private readonly IUserRepository _users;

    public AuthenticationService(IUserRepository users)
    {
        _users = users;
    }

    public User? Authenticate(string username, string password)
    {
        User? user = _users.GetByUsername(username);
        return user is not null && user.Password == password ? user : null;
    }
}
