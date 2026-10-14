using TaskMaster.Domain;

namespace TaskMaster.Application.Abstractions.Persistence;

public interface IUserRepository
{
    bool ExistsByUsername(string username);
    bool Add(User user);
    User? GetByUsername(string username);
    int Update(string username, string field, string value);
    bool Delete(string username);
}
