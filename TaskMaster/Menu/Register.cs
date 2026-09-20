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

        string? name = ConsoleHelper.ReadInput(
            "Name (B + Enter = Back): "
        );

        if(name == null)
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
        
        while(!SystemValidation.System.StringValidationInput(
            name,
            out string? errorMessage))
        {
            ConsoleHelper.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );
            
            name = ConsoleHelper.ReadInput(
                "Name (B + Enter = Back): "
            );


            if(name == null)
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
        
        string? lastName = ConsoleHelper.ReadInput(
            "Last Name (B + Enter = Back): "
        );
        
        if(lastName == null)
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

        string? username = ConsoleHelper.ReadInput(
            "Username (B + Enter = Back): "
        );


        if(username == null)
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
        
        string? password = ConsoleHelper.ReadInput(
            "Password (B + Enter = Back): "
        );
        
        if(password == null)
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

            return null;
        }
        
        ConsoleHelper.PrintColorizeMessage(
            "Register Successful!",
            ConsoleColor.Green
        );

        Console.ReadLine();
        
        return user;
    }
}