using DataBase;
namespace UserModel;

public enum Level { Beginner, MidLevel, Advanced }
public enum Role { Designer, Intern }
public class User
{
    public Role Role { get; private set; }
    public string? Name { get; private set; }
    public string? LastName { get; private set; }
    public string? UserName { get; private set; }
    public string? Password { get; private set; }
    public Level Level { get; private set; }
    public int Score { get; private set; }
    private static List<User> GetUsers() => Database.Load<User>(DataType.Users);
    public User(Role role, string name, string lastName, string userName, string password, Level level, int score)
    {
        Role = role;
        Name = name;
        LastName = lastName;
        UserName = userName;
        Password = password;
        Level = level;
        Score = score;
    }
    public bool Add()
    {
        List<User> users = GetUsers();
        if (users.Any(u => string.Equals(u.UserName, UserName, StringComparison.OrdinalIgnoreCase)))
            return false;
        
        Database.Save(this, DataType.Users);
        return true;
    }
    public bool Edit(string field, string data)
    {
        List<User> users = GetUsers();
        User? user = users.Find(u => u.UserName == UserName);
        if (user is null) return false;

        switch (field)
        {
            case "Name":
                user.Name = data;
                Name = data;
                break;
            case "LastName":
                user.LastName = data;
                LastName = data;
                break;
            case "UserName":
                if (users.Any(u => u != user && string.Equals(u.UserName, data, StringComparison.OrdinalIgnoreCase)))
                    return false;
                user.UserName = data;
                UserName = data;
                break;
            case "Password":
                user.Password = data;
                Password = data;
                break;
            default:
                return false;
        }

        Database.Update(users, DataType.Users);
        return true;
    }
    public bool Remove()
    {
        List<User> users = GetUsers();
        User? user = users.Find(u => u.UserName == UserName);
        if (user is null) return false;

        users.Remove(user);
        Database.Update(users, DataType.Users);
        return true;
    }
    public static void SetScore(string username, int grade)
    {
        List<User> users = GetUsers();
        User user = users.FirstOrDefault(u => u.UserName == username)!;
        user.Score += grade;
        Database.Update(users, DataType.Users);
    }
}