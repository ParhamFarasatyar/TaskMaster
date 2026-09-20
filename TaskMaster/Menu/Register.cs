using DataBase;
using UserModel;

namespace Menu;

public class Register
{
    public User? Create()
    {
        return SelectRole();
    }



    private User? SelectRole()
    {
        Console.Clear();


        string[] roles =
        {
            Role.Intern.ToString(),
            Role.Designer.ToString(),
            "Back"
        };


        int selectedRole = ConsoleMenu.Show(
            "    Select Role",
            roles
        );


        if(selectedRole == -1 || selectedRole == 2)
        {
            return null;
        }



        Role role = selectedRole switch
        {
            0 => Role.Intern,
            1 => Role.Designer,
            _ => Role.Intern
        };


        return GetName(role);
    }




    private User? GetName(Role role)
    {
        Console.Clear();


        string? name = ConsoleHelper.ReadInput(
            "Name (B + Enter = Back): "
        );


        if(name == null)
        {
            return SelectRole();
        }



        if(!SystemValidation.System.StringValidationInput(
            name,
            out string? errorMessage))
        {
            ConsoleHelper.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );


            Console.ReadLine();

            return GetName(role);
        }



        return GetLastName(
            role,
            name
        );
    }





    private User? GetLastName(
        Role role,
        string name)
    {
        Console.Clear();


        string? lastName = ConsoleHelper.ReadInput(
            "Last Name (B + Enter = Back): "
        );


        if(lastName == null)
        {
            return GetName(role);
        }



        if(!SystemValidation.System.StringValidationInput(
            lastName,
            out string? errorMessage))
        {
            ConsoleHelper.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );


            Console.ReadLine();

            return GetLastName(
                role,
                name
            );
        }



        return GetUsername(
            role,
            name,
            lastName
        );
    }





    private User? GetUsername(
        Role role,
        string name,
        string lastName)
    {
        Console.Clear();


        string? username = ConsoleHelper.ReadInput(
            "Username (B + Enter = Back): "
        );


        if(username == null)
        {
            return GetLastName(
                role,
                name
            );
        }



        if(!SystemValidation.System.ValidateUsername(
            username,
            out string? errorMessage))
        {
            ConsoleHelper.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );


            Console.ReadLine();

            return GetUsername(
                role,
                name,
                lastName
            );
        }



        return GetPassword(
            role,
            name,
            lastName,
            username
        );
    }





    private User? GetPassword(
        Role role,
        string name,
        string lastName,
        string username)
    {
        Console.Clear();


        string? password = ConsoleHelper.ReadInput(
            "Password (B + Enter = Back): "
        );


        if(password == null)
        {
            return GetUsername(
                role,
                name,
                lastName
            );
        }



        if(!SystemValidation.System.ValidateUserPassword(
            password,
            out string? errorMessage))
        {
            ConsoleHelper.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );


            Console.ReadLine();

            return GetPassword(
                role,
                name,
                lastName,
                username
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
                "Register failed!\nUsername already exists.",
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