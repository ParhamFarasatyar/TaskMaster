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
    private static List<User>? GetUsers() => Database.LoadUser();
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
    public bool Edit(string field, string data)
    {
        List<User>? users = GetUsers();
        User? user = users?.Find(u => u.UserName == UserName);
        if (user is null) return false;

        Database.UpdateUser(user.UserName!, field, data);
        return true;
    }
    public bool Remove()
    {
        List<User>? usersId = GetUsers();
        User? user = usersId?.Find(u => u.UserName == UserName);
        if (user is null) return false;

        Database.DeleteUser(Convert.ToInt32(user));

        return true;
    }
    public static void SetScore(string username, int grade)
    {
        List<User>? users = GetUsers();
        User user = users!.FirstOrDefault(u => u.UserName == username)!;
        user.Score += grade;
        Database.UpdateUser(user.UserName!, "Grade", user.Score.ToString());
    }
    public static void UpdateLevel(string username)
    {
        List<User> users = GetUsers()!;
        User user = users.FirstOrDefault(u => u.UserName == username)!;
        if (user.Score <= 10) user.Level = Level.Beginner;
        else if (user.Score > 10 && user.Score <= 20) user.Level = Level.MidLevel;
        else user.Level = Level.Advanced;
        Database.UpdateUser(user.UserName!, "Level", ((int)user.Level).ToString());
    }
}