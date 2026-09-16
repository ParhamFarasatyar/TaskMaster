using UserModel;
namespace Menu;

public class InternProfile
{
    public void Show(User user)
    {
        Console.Clear();
        
        Console.WriteLine("====================");
        Console.WriteLine("    INTERN INFO     ");
        Console.WriteLine("====================");
        
        Console.WriteLine(
            $"Full Name : {user.Name} {user.LastName}"
        );
        
        Console.WriteLine(
            $"Username  : {user.UserName}"
        );
        
        Console.WriteLine(
            $"Level     : {user.Level}"
        );
        
        Console.WriteLine(
            $"Score     : {user.Score}"
        );
        
        Console.WriteLine();
        Console.WriteLine("Press Enter to return...");
        Console.ReadLine();
    }
}