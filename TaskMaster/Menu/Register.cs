using DataBase;
using UserModel;

namespace Menu;

public class Register
{
    public User Create()
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
            "Select Role",
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


        Console.Write("Last Name: ");
        string lastName = Console.ReadLine()!;


        Console.Write("Username: ");
        string username = Console.ReadLine()!;


        Console.Write("Password: ");
        string password = Console.ReadLine()!;



        User user = new User(
            role,
            name,
            lastName,
            username,
            password,
            Level.Beginner,
            0
        );


        Database.Save(
            user,
            DataType.Users
        );


        Console.WriteLine();
        Console.WriteLine("Register Successful!");
        Thread.Sleep(2000);
        
        return user;
    }
}