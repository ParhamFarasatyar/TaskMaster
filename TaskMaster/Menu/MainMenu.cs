namespace Menu;
using UserModel;
public class MainMenu
{
    public void Show()
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
                Login();
                break;


            case 1:
                Register();
                break;


            case 2:
                Exit();
                break;
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