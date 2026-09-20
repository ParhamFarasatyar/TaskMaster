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
    private List<User> GetUsers() => Database.Load<User>(DataType.Users);
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
        if (users.Any(u => u.UserName == UserName)) return false;
        
        Database.Save(this, DataType.Users);
        return true;
    }
    public bool Edit(string field, string data)
    {
        List<User> users = GetUsers();
        User? user = users.Find(u => u.UserName == UserName);
        if (user is null) return false;

        var properties = typeof(User).GetProperties();
        foreach (var property in properties)
        {
            if (property.Name == field && property.Name != "UserName") property.SetValue(user, data);
            else if (property.Name is "UserName" && field == "Username")
            {
                if(users.FirstOrDefault(u => u.UserName == data) is null)
                {
                    user.UserName = data;
                }
                else return false;
            }
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
}