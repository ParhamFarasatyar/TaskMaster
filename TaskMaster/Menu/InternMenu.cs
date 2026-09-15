using UserModel;
namespace Menu;

public class InternMenu
{
    public void Show(User user)
    {
        Console.WriteLine("======================");
        Console.WriteLine("      INTERN MENU     ");
        Console.WriteLine("======================");

        Console.WriteLine("1. Profile Info");
        Console.WriteLine("2. Answer Question");
        Console.WriteLine("3. Answers Status");
        Console.WriteLine("4. Logout");

        Console.Write("Choose option: ");

        string? choice = Console.ReadLine();


        switch(choice)
        {
            case "1":
                // Show intern profile
                Console.WriteLine("Profile Info Selected");
                break;


            case "2":
                // Answer question logic is not implemented here
                Console.WriteLine("Answer Question Selected");
                break;


            case "3":
                // Show answers status logic is not implemented here
                Console.WriteLine("Answers Status Selected");
                break;


            case "4":
                Console.WriteLine("Logout Selected");
                break;


            default:
                // Validation will be handled by another module
                Console.WriteLine("Invalid option");
                break;
        }
    }
}