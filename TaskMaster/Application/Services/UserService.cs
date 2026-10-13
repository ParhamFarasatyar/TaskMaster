using TaskMaster.Application.Abstractions.Persistence;
using TaskMaster.Domain;

namespace TaskMaster.Application.Services;

public sealed class UserService
{
    private readonly IUserRepository _users;

    public UserService(IUserRepository users)
    {
        _users = users;
    }

    public bool Update(User user, string field, string value)
    {
        if (_users.Update(user.UserName, field, value) != 1)
        {
            return false;
        }

        user.UpdateProfile(field, value);
        return true;
    }

    public bool Delete(User user) => _users.Delete(user.UserName);
}
