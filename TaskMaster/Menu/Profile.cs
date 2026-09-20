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

            Console.WriteLine($"Full Name : {user.Name} {user.LastName}");
            Console.WriteLine($"Username  : {user.UserName}");
            if (user.Role == Role.Intern)
            {
                Console.WriteLine($"Level     : {user.Level}");
                Console.WriteLine($"Score     : {user.Score}");
            }
            Console.WriteLine();

            string[] options = {"Edit Profile", "Delete Account", "Back"};

            int selected = ConsoleMenu.Show("", options);

            if (selected == -1 || selected == 2) return false;

            switch (selected)
            {
                case 0:
                    EditProfile(user);
                    break;

                case 1:
                    if(DeleteAccount(user)) return true;
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

            int selected = ConsoleMenu.Show("Edit Profile", fields);

            if (selected == -1 || selected == 4) return;

            string field = fields[selected];

            string? value = ConsoleHelper.ReadInput($"New {field} (B + Enter = Back): ");

            if (value?.ToLower() != "b")
            {
                if (field is "UserName")
                {
                    bool isValid = SystemValidation.System.ValidateUsername(value, out string? errorMessage);
                    while (!isValid)
                    {
                        SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
                        value = ConsoleHelper.ReadInput($"New {field} (B + Enter = Back): ");
                        isValid = SystemValidation.System.ValidateUsername(value, out errorMessage);
                    }
                }
                else if (field is "Password")
                {
                    bool isValid = SystemValidation.System.ValidateUserPassword(value, out string? errorMessage);
                    while (!isValid)
                    {
                        SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
                        value = ConsoleHelper.ReadInput($"New {field} (B + Enter = Back): ");
                        isValid = SystemValidation.System.ValidateUserPassword(value, out errorMessage);
                    }
                }
                else
                {
                    bool isValid = SystemValidation.System.StringValidationInput(value, out string? errorMessage);
                    while (!isValid)
                    {
                        SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
                        value = ConsoleHelper.ReadInput($"New {field} (B + Enter = Back): ");
                        isValid = SystemValidation.System.StringValidationInput(value, out errorMessage);
                    }
                }
            }
            else return;

            bool result = user.Edit(field, value!);
            if (result)
            ConsoleHelper.PrintColorizeMessage("Profile Updated Successfully!", ConsoleColor.Green);
            else
            ConsoleHelper.PrintColorizeMessage("Update Failed!", ConsoleColor.Red);
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

        if(selected is -1 || selected is 1) return false;

        bool result = user.Remove();
        if (result)
        {
            ConsoleHelper.PrintColorizeMessage("Account Deleted Successfully!", ConsoleColor.Green);
            Console.ReadLine();
            return true;
        }
        return false;
    }
}