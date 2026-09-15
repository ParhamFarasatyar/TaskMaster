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
        List<User> users = Database.Load<User>(DataType.Users);
        if (users.Any(u => u.UserName == UserName)) return false;
        
        Database.Save(this, DataType.Users);
        return true;
    }
    public bool Edit(string field, string data)
    {
        List<User> users = Database.Load<User>(DataType.Users);
        int selectedUserIndex = users.FindIndex(u => u.UserName == UserName);
        if (selectedUserIndex == -1) return false;

        switch (field)
        {
            case "Name":
                users[selectedUserIndex].Name = data;
            break;

            case "LastName":
                users[selectedUserIndex].LastName = data;
            break;

            case "UserName":
                if (users.Any(u => u.UserName != data)) users[selectedUserIndex].UserName = data;
                else return false;
            break;

            case "Password":
                users[selectedUserIndex].Password = data;
            break;
        }

        Database.Update(users, DataType.Users);
        return true;
    }
    public bool Remove()
    {
        List<User> users = Database.Load<User>(DataType.Users);
        int selectedUserIndex = users.FindIndex(u => u.UserName == UserName);
        if (selectedUserIndex == -1) return false;

        users.RemoveAt(selectedUserIndex);
        Database.Update(users, DataType.Users);
        return true;
    }
}