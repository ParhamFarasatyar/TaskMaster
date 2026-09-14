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
    public float Score { get; private set; }
    public User(Role role, string name, string lastName, string userName, string password, Level level, float score)
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
        List<User> users = Database.Load<User>(DataType.Users);
        if (users.Any(u => u.UserName == UserName)) return false;
        
        Database.Save(this, DataType.Users);
        return true;
    }
    public void Edit()
    {
        /* Edit existed user */
    }
    public void Remove()
    {
        /* Remove existed user */
    }
}