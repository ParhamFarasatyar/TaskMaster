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


        Console.Write("Password: ");
        string password = Console.ReadLine()!;



        List<User> users =
            Database.Load<User>(DataType.Users);



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
}