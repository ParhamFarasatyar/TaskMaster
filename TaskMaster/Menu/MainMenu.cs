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

                User? loginUser = login.Enter();
                
                if(loginUser != null &&
                   loginUser.UserName == "__BACK__")
                {
                    return Show();
                }
                
                return loginUser;
            
            case 1:

                Register register = new();
                
                User? registerUser = register.Create();
                
                if(registerUser != null &&
                   registerUser.UserName == "__BACK__")
                {
                    return Show();
                }
                
                return registerUser;
            
            case 2:

                return null;
            
            default:

                return null;
        }
    }
}