using DataBase;
using UserModel;
namespace Menu;

public class Register
{
    public User? Create()
    {
        Console.Clear();

        Console.WriteLine("====================");
        Console.WriteLine("      REGISTER      ");
        Console.WriteLine("====================");
        
        string[] roles =
        {
            Role.Intern.ToString(),
            Role.Designer.ToString(),
            "Back"
        };
        
        int selectedRole = ConsoleMenu.Show(
            "Select Role",
            roles
        );
        
        if(selectedRole == -1 || selectedRole == 2)
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
        
        Role role = selectedRole switch
        {
            0 => Role.Intern,
            1 => Role.Designer,
            _ => Role.Intern
        };
        
        Console.Clear();

        string? name = ReadValue("Name (B + Enter = Back): ", ValidateName);
        if (name == null) return BackUser();

        string? lastName = ReadValue("Last Name (B + Enter = Back): ", ValidateName);
        if (lastName == null) return BackUser();

        string? username = ReadValue("Username (B + Enter = Back): ", ValidateUsername);
        if (username == null) return BackUser();

        string? password = ReadValue("Password (B + Enter = Back): ", ValidatePassword, true);
        if (password == null) return BackUser();

        User user = new User(
            role,
            name,
            lastName,
            username,
            password,
            Level.Beginner,
            0
        );

        bool isRegistered = user.Add();

        if(!isRegistered)
        {
            ConsoleHelper.PrintColorizeMessage(
                "Register failed! Username already exists.",
                ConsoleColor.Red
            );

            Console.ReadLine();

            return BackUser();
        }
        
        ConsoleHelper.PrintColorizeMessage(
            "Register Successful!",
            ConsoleColor.Green
        );

        Console.ReadLine();
        
        return user;
    }

    private static string? ReadValue(
        string prompt,
        Func<string, (bool IsValid, string? Error)> validator,
        bool hideInput = false)
    {
        while (true)
        {
            string? value = ConsoleHelper.ReadInput(prompt, hideInput);
            if (value == null) return null;

            (bool isValid, string? error) = validator(value);
            if (isValid) return value;

            ConsoleHelper.PrintColorizeMessage(error!, ConsoleColor.Red);
        }
    }

    private static (bool, string?) ValidateName(string value)
    {
        bool valid = SystemValidation.System.StringValidationInput(value, out string? error);
        return (valid, error);
    }

    private static (bool, string?) ValidateUsername(string value)
    {
        bool valid = SystemValidation.System.ValidateUsername(value, out string? error);
        return (valid, error);
    }

    private static (bool, string?) ValidatePassword(string value)
    {
        bool valid = SystemValidation.System.ValidateUserPassword(value, out string? error);
        return (valid, error);
    }

    private static User BackUser()
    {
        return new User(Role.Intern, "", "", "__BACK__", "", Level.Beginner, 0);
    }
}