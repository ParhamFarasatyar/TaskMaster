namespace TaskMaster.Domain;

public enum Level { Beginner, MidLevel, Advanced }
public enum Role { Designer, Intern }
public sealed class User
{
    public Role Role { get; private set; }
    public string Name { get; private set; }
    public string LastName { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public Level Level { get; private set; }
    public int Score { get; private set; }
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

    public void UpdateProfile(string field, string value)
    {
        switch (field)
        {
            case "Name":
                Name = value;
                break;
            case "LastName":
                LastName = value;
                break;
            case "UserName":
                UserName = value;
                break;
            case "Password":
                Password = value;
                break;
            default:
                throw new ArgumentException("Unsupported profile field.", nameof(field));
        }
    }

}