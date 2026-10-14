namespace TaskMaster.Presentation.Cli.Menus;
using TaskMaster.Application.Services;
using TaskMaster.Domain;

public class MainMenu
{
    private readonly UserRegistrationService _registrationService;
    private readonly AuthenticationService _authenticationService;

    public MainMenu(
        UserRegistrationService registrationService,
        AuthenticationService authenticationService)
    {
        _registrationService = registrationService;
        _authenticationService = authenticationService;
    }

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

                Login login = new(_authenticationService);

                User? loginUser = login.Enter();
                
                if(loginUser != null &&
                   loginUser.UserName == "__BACK__")
                {
                    return Show();
                }
                
                return loginUser;
            
            case 1:

                Register register = new(_registrationService);
                
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