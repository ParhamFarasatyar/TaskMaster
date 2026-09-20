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



        Console.Write("Username: ");
        string username = Console.ReadLine()!;

        bool isValid = SystemValidation.System.ValidateUsername(
            username,
            out string? errorMessage
        );


        while (!isValid)
        {
            SystemValidation.System.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );


            Console.Write("Username: ");

            username = Console.ReadLine()!;


            isValid = SystemValidation.System.ValidateUsername(
                username,
                out errorMessage
            );
        }




        Console.Write("Password: ");

        string password = Console.ReadLine()!;


        isValid = SystemValidation.System.ValidateUserPassword(
            password,
            out errorMessage
        );


        while (!isValid)
        {
            SystemValidation.System.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );


            Console.Write("Password: ");

            password = Console.ReadLine()!;


            isValid = SystemValidation.System.ValidateUserPassword(
                password,
                out errorMessage
            );
        }




        List<User> users =
            Database.Load<User>(DataType.Users);



        User? user = users.FirstOrDefault(
            u =>
                u.UserName == username &&
                u.Password == password
        );



        if (user == null)
        {
            ConsoleHelper.PrintColorizeMessage(
                "Wrong username or password",
                ConsoleColor.Red
            );


            Console.WriteLine(
                "Press Enter to try again..."
            );

            Console.ReadLine();


            return Enter();
        }



        return user;
    }
}