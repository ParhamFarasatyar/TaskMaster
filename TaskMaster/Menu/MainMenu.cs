namespace Menu;
using UserModel;
public class MainMenu
{
    public User? Show()
    {
        string[] options =
        {
            "Login",
            "Register",
            "Exit"
        };


        int selected = ConsoleMenu.Show(
            new string(' ', 4) + "Task Master",
            options
        );


        switch (selected)
        {
            case 0:
                Login login = new();
                return login.Enter();


            case 1:
                Register register = new();
                return register.Create();


            case 2:
                return null;
            
            default:
                return null;
        }
    }



    private void Login()
    {
        Login login = new();

        User? user = login.Enter();


        if(user != null)
        {
            MenuManager manager = new();

            manager.Show(user);
        }
        else
        {
            Show();
        }
    }



    private void Register()
    {
        Register register = new();

        User? user = register.Create();

        if (user == null)
        {
            MainMenu mainMenu = new();
            mainMenu.Show();
            return;
        }

        MenuManager manager = new();

        manager.Show(user!);
    }


    
    private void Exit()
    {
        Console.WriteLine("Goodbye!");
    }
}