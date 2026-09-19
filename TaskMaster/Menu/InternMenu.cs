using UserModel;
namespace Menu;

public class InternMenu
{
    public void Show(User user)
    {
        bool exit = false;


        while (!exit)
        {
            string[] options =
            {
                "Profile Info",
                "Answer Question",
                "Answers Status",
                "Logout"
            };


            int selected = ConsoleMenu.Show(
                "    Intern Menu",
                options
            );


            switch(selected)
            {
                case 0:

                    InternProfile profile = new();

                    profile.Show(user);

                    break;



                case 1:

                    AnswerQuestion answerQuestion = new();

                    answerQuestion.Answer();

                    break;



                case 2:

                    AnswersStatus answersStatus = new();

                    answersStatus.Show();

                    break;



                case 3:

                    exit = true;

                    break;
            }
        }


        Console.Clear();

        MainMenu mainMenu = new();

        mainMenu.Show();
    }
}