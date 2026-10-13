using TaskMaster.Application.Services;
using TaskMaster.Domain;
using TaskMaster.Presentation.Cli.Validation;
namespace TaskMaster.Presentation.Cli.Menus;

public class Login
{
    private readonly AuthenticationService _authenticationService;

    public Login(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public User? Enter()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
            ConsoleHelper.PrintColorizeMessage("        LOGIN       ", ConsoleColor.White);
            ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);

            string? username = ReadUsername();
            if (username == null) return BackUser();

            string? password = ReadPassword();
            if (password == null) return BackUser();

            User? user = _authenticationService.Authenticate(username, password);

            if (user != null) return user;

            ConsoleHelper.PrintColorizeMessage(
                "Wrong username or password",
                ConsoleColor.Red
            );
            ConsoleHelper.Countdown();
        }
    }

    private string? ReadUsername()
    {
        while (true)
        {
            string? value = ConsoleHelper.ReadInput("Username: ");
            if (value == null) return null;

            if (InputValidator.ValidateUsername(value, out string? errorMessage))
                return value;

            ConsoleHelper.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
        }
    }

    private string? ReadPassword()
    {
        while (true)
        {
            string? value = ConsoleHelper.ReadInput("Password: ", true);
            if (value == null) return null;

            if (InputValidator.ValidateUserPassword(value, out string? errorMessage))
                return value;

            ConsoleHelper.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
        }
    }
    
    private User BackUser()
    {
        return new User(
            Role.Intern,
            "",
            "",
            "__BACK__",
            "",
            Level.Beginner,
            0
        );
    }
}