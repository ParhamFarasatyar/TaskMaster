using UserModel;
namespace Menu;

public class Profile
{
    public bool Show(User user)
    {
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine("====================");
            Console.WriteLine("       PROFILE      ");
            Console.WriteLine("====================");
            
            Console.WriteLine();
            
            Console.WriteLine(
                $"Role      : {user.Role}"
            );
            
            Console.WriteLine(
                $"Full Name : {user.Name} {user.LastName}"
            );
            
            Console.WriteLine(
                $"Username  : {user.UserName}"
            );
            
            if (user.Role == Role.Intern)
            {
                Console.WriteLine(
                    $"Level     : {user.Level}"
                );

                Console.WriteLine(
                    $"Score     : {user.Score}"
                );
            }
            
            Console.WriteLine();
            
            string[] options =
            {
                "Edit Profile",
                "Delete Account",
                "Back"
            };
            
            int selected = ConsoleMenu.Show(
                "Profile Options",
                options,
                true,
                false
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
                $"New {field} (B = Back): "
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
                        SystemValidation.System.ValidateUsername(
                            value,
                            out errorMessage
                        );

                    break;
                
                case "Password":

                    isValid =
                        SystemValidation.System.ValidateUserPassword(
                            value,
                            out errorMessage
                        );

                    break;
                
                default:

                    isValid =
                        SystemValidation.System.StringValidationInput(
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
                    $"New {field} (B = Back): "
                );
                
                if (value == null)
                {
                    break;
                }
                
                switch (field)
                {
                    case "UserName":

                        isValid =
                            SystemValidation.System.ValidateUsername(
                                value,
                                out errorMessage
                            );

                        break;
                    
                    case "Password":

                        isValid =
                            SystemValidation.System.ValidateUserPassword(
                                value,
                                out errorMessage
                            );

                        break;
                    
                    default:

                        isValid =
                            SystemValidation.System.StringValidationInput(
                                value,
                                out errorMessage
                            );

                        break;
                }
            }
            
            bool result = user.Edit(
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
            
            Thread.Sleep(2000);
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
        
        bool result = user.Remove();
        
        if (result)
        {
            ConsoleHelper.PrintColorizeMessage(
                "Account Deleted Successfully!",
                ConsoleColor.Green
            );
            
            Console.ReadLine();
            
            return true;
        }
        
        return false;
    }
}