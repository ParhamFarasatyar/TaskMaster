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
            Role.Designer.ToString()
        };


        int selectedRole = ConsoleMenu.Show(
            "    Select Role",
            roles
        );


        Role role = selectedRole switch
        {
            0 => Role.Intern,
            1 => Role.Designer,
            _ => Role.Intern
        };


        Console.Clear();


        Console.Write("Name: ");
        string name = Console.ReadLine()!;
        bool isValid = SystemValidation.System.StringValidationInput(name, out string? errorMessage);
        while (!isValid)
        {
            SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
            Console.Write("Name: ");
            name = Console.ReadLine()!;
            isValid = SystemValidation.System.StringValidationInput(name, out errorMessage);
        }


        Console.Write("Last Name: ");
        string lastName = Console.ReadLine()!;
        isValid = SystemValidation.System.StringValidationInput(lastName, out errorMessage);
        while (!isValid)
        {
            SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
            Console.Write("Last Name: ");
            lastName = Console.ReadLine()!;
            isValid = SystemValidation.System.StringValidationInput(lastName, out errorMessage);
        }


        Console.Write("Username: ");
        string username = Console.ReadLine()!;
        isValid = SystemValidation.System.ValidateUsername(username, out errorMessage);
        while (!isValid)
        {
            SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
            Console.Write("Username: ");
            username = Console.ReadLine()!;
            isValid = SystemValidation.System.ValidateUsername(username, out errorMessage);
        }


        Console.Write("Password: ");
        string password = Console.ReadLine()!;
        isValid = SystemValidation.System.ValidateUserPassword(password, out errorMessage);
        while (!isValid)
        {
            SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
            Console.Write("Password: ");
            password = Console.ReadLine()!;
            isValid = SystemValidation.System.ValidateUserPassword(password, out errorMessage);
        }

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

        if (!isRegistered)
        {
            Console.WriteLine();
            Console.WriteLine("Register failed!\nYour username is exist already.");
            Thread.Sleep(2000);
            return null;
        }


        Console.WriteLine();
        Console.WriteLine("Register Successful!");
        Thread.Sleep(2000);
        
        return user;
    }
}