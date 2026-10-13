using Services;
using UserModel;

namespace DataBase;

public sealed class SqlUserRepository : IUserRepository
{
    public bool ExistsByUsername(string username)
    {
        List<User> users = Database.LoadUser()
            ?? throw new InvalidOperationException(
                "Could not load users to check username availability."
            );

        return users.Any(user =>
            string.Equals(
                user.UserName,
                username,
                StringComparison.OrdinalIgnoreCase
            )
        );
    }

    public bool Add(User user)
    {
        return Database.SaveUser(user);
    }
}
