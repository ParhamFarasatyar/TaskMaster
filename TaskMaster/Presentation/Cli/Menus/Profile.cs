using TaskMaster.Application.Services;
using TaskMaster.Domain;
using TaskMaster.Presentation.Cli.Validation;
namespace TaskMaster.Presentation.Cli.Menus;

public class Profile
{
    private readonly UserService _usersService;

    public Profile(UserService usersService)
    {
        _usersService = usersService;
    }

    public bool Show(User user)
    {
        while (true)
        {
            string[] options =
            {
                "Edit Profile",
                "Delete Account",
                "Back"
            };

            string profileInfo =
                $"Profile\n\n" +
                $"Role      : {user.Role}\n" +
                $"Full Name : {user.Name} {user.LastName}\n" +
                $"Username  : {user.UserName}\n" +
                "Password  : (hidden)";

            if (user.Role == Role.Intern)
            {
                profileInfo +=
                    $"\nLevel     : {user.Level}" +
                    $"\nScore     : {user.Score}";
            }
            
            int selected = ConsoleMenu.Show(
                profileInfo + "\n\nProfile Options",
                options
            );
            
            if (selected == -1 || selected == 2)
            {
                return false;
            }
            
            switch (selected)
            {
                case 0:

                    EditProfile(user);

                    break;
                
                case 1:

                    bool deleted = DeleteAccount(user);
                    
                    if (deleted)
                    {
                        return true;
                    }
                    
                    break;
            }
        }
    }
    
    private void EditProfile(User user)
    {
        while (true)
        {
            string[] fields =
            {
                "Name",
                "LastName",
                "UserName",
                "Password",
                "Back"
            };
            
            int selected = ConsoleMenu.Show(
                "Edit Profile",
                fields
            );
            
            if (selected == -1 || selected == 4)
            {
                return;
            }
            
            string field = fields[selected];
            
            string? value = ConsoleHelper.ReadInput(
                $"New {field}: ",
                field == "Password"
            );
            
            if (value == null)
            {
                continue;
            }
            
            bool isValid = true;
            string? errorMessage = null;
            
            switch (field)
            {
                case "UserName":

                    isValid =
                        InputValidator.ValidateUsername(
                            value,
                            out errorMessage
                        );

                    break;
                
                case "Password":

                    isValid =
                        InputValidator.ValidateUserPassword(
                            value,
                            out errorMessage
                        );

                    break;
                
                default:

                    isValid =
                        InputValidator.StringValidationInput(
                            value,
                            out errorMessage
                        );

                    break;
            }
            
            while (!isValid)
            {
                ConsoleHelper.PrintColorizeMessage(
                    errorMessage!,
                    ConsoleColor.Red
                );
                
                value = ConsoleHelper.ReadInput(
                    $"New {field}: ",
                    field == "Password"
                );
                
                if (value == null)
                {
                    break;
                }
                
                switch (field)
                {
                    case "UserName":

                        isValid =
                            InputValidator.ValidateUsername(
                                value,
                                out errorMessage
                            );

                        break;
                    
                    case "Password":

                        isValid =
                            InputValidator.ValidateUserPassword(
                                value,
                                out errorMessage
                            );

                        break;
                    
                    default:

                        isValid =
                            InputValidator.StringValidationInput(
                                value,
                                out errorMessage
                            );

                        break;
                }
            }

            if (value == null)
            {
                continue;
            }
            
            bool result = _usersService.Update(
                user,
                field,
                value!
            );
            
            if (result)
            {
                ConsoleHelper.PrintColorizeMessage(
                    "Profile Updated Successfully!",
                    ConsoleColor.Green
                );
            }
            else
            {
                ConsoleHelper.PrintColorizeMessage(
                    "Update Failed!",
                    ConsoleColor.Red
                );
            }
            
            ConsoleHelper.Countdown();
        }
    }
    
    private bool DeleteAccount(User user)
    {
        string[] options =
        {
            "Yes, Delete",
            "No, Back"
        };
        
        int selected = ConsoleMenu.Show(
            "Delete Account?",
            options
        );
        
        if (selected == -1 || selected == 1)
        {
            return false;
        }
        
        bool result = _usersService.Delete(user);
        
        if (result)
        {
            ConsoleHelper.PrintColorizeMessage(
                "Account Deleted Successfully!",
                ConsoleColor.Green
            );
            
            ConsoleHelper.Countdown();
            
            return true;
        }
        
        return false;
    }
}