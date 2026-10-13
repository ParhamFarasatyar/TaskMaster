using UserModel;

namespace Services;

public interface IUserRepository
{
    bool ExistsByUsername(string username);
    bool Add(User user);
}
