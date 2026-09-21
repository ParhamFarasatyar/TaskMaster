using DataBase;
using UserModel;
namespace Menu;

public class Login
{
    public User? Enter()
    {
        Console.Clear();
        
        Console.WriteLine("====================");
        Console.WriteLine("        LOGIN       ");
        Console.WriteLine("====================");
        
        string? username = ConsoleHelper.ReadInput(
            "Username (B = Back): "
        );
        
        if(username == null)
        {
            return BackUser();
        }
        
        bool isValid =
            SystemValidation.System.ValidateUsername(
                username,
                out string? errorMessage
            );
        
        while(!isValid)
        {
            SystemValidation.System.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );
            
            username = ConsoleHelper.ReadInput(
                "Username (B = Back): "
            );
            
            if(username == null)
            {
                return BackUser();
            }
            
            isValid =
                SystemValidation.System.ValidateUsername(
                    username,
                    out errorMessage
                );
        }
        
        string? password = ConsoleHelper.ReadInput(
            "Password (B = Back): "
        );
        
        if(password == null)
        {
            return BackUser();
        }

        isValid =
            SystemValidation.System.ValidateUserPassword(
                password,
                out errorMessage
            );

        while(!isValid)
        {
            SystemValidation.System.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );
            
            password = ConsoleHelper.ReadInput(
                "Password (B = Back): "
            );
            
            if(password == null)
            {
                return BackUser();
            }
            
            isValid =
                SystemValidation.System.ValidateUserPassword(
                    password,
                    out errorMessage
                );
        }
        
        List<User> users =
            Database.Load<User>(
                DataType.Users
            );
        
        User? user = users.FirstOrDefault(
            u =>
                u.UserName == username &&
                u.Password == password
        );
        
        if(user == null)
        {
            ConsoleHelper.PrintColorizeMessage(
                "Wrong username or password",
                ConsoleColor.Red
            );
            
            Console.ReadLine();
            
            return Enter();
        }
        
        return user;
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