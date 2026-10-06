using Menu;
using UserModel;


namespace TaskMaster;

class Program
{
    static void Main(string[] args)
    {
        while(true)
        {
            MainMenu mainMenu = new();


            User? user = mainMenu.Show();
            
            if(user == null)
            {
                ConsoleHelper.PrintColorizeMessage("Goodbye!", ConsoleColor.White);

                break;
            }



            if(user.Role == Role.Intern)
            {
                InternMenu internMenu = new();

                bool shouldContinue = internMenu.Show(user);
                if (!shouldContinue)
                {
                    continue;
                }
            }



            else if(user.Role == Role.Designer)
            {
                DesignerMenu designerMenu = new();

                bool shouldContinue = designerMenu.Show(user);
                if (!shouldContinue)
                {
                    continue;
                }
            }
        }
    }
}