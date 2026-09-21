using DataBase;
using UserModel;
namespace Menu;

public class Login
{
    public User? Enter()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("====================");
            Console.WriteLine("        LOGIN       ");
            Console.WriteLine("====================");

            string? username = ReadUsername();
            if (username == null) return BackUser();

            string? password = ReadPassword();
            if (password == null) return BackUser();

            List<User> users = Database.Load<User>(DataType.Users);
            User? user = users.FirstOrDefault(
                u => string.Equals(u.UserName, username, StringComparison.OrdinalIgnoreCase)
                     && u.Password == password
            );

            if (user != null) return user;

            ConsoleHelper.PrintColorizeMessage(
                "Wrong username or password",
                ConsoleColor.Red
            );
            Console.ReadLine();
        }
    }

    private string? ReadUsername()
    {
        while (true)
        {
            string? value = ConsoleHelper.ReadInput("Username (B = Back): ");
            if (value == null) return null;

            if (SystemValidation.System.ValidateUsername(value, out string? errorMessage))
                return value;

            ConsoleHelper.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
        }
    }

    private string? ReadPassword()
    {
        while (true)
        {
            string? value = ConsoleHelper.ReadInput("Password (B = Back): ", true);
            if (value == null) return null;

            if (SystemValidation.System.ValidateUserPassword(value, out string? errorMessage))
                return value;

            ConsoleHelper.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
        }
    }
    
    private User BackUser()
    {
        return new User(
            Role.Intern,
            "",
            "",
            "__BACK__",
            "",
            Level.Beginner,
            0
        );
    }
}