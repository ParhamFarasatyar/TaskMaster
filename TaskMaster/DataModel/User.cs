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
}