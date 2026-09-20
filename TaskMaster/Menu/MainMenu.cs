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
        
        if(selected == -1)
        {
            return null;
        }
        
        switch(selected)
        {
            case 0:

                Login login = new();

                return login.Enter();
            
            case 1:

                Register register = new();
                
                User? user = register.Create();

                if(user != null &&
                   user.UserName == "__BACK__")
                {
                    return Show();
                }
                
                return user;
            
            case 2:

                return null;
            
            default:

                return null;
        }
    }
}