using UserModel;

namespace Menu;

public class DesignerMenu
{
    public void Show(User user)
    {
        bool exit = false;


        while (!exit)
        {
            string[] options =
            {
                "Back to Main Menu",
                "Add Question",
                "Edit Question",
                "Remove Question",
                "Review Answers",
                "Logout"
            };


            int selected = ConsoleMenu.Show(
                "Designer Menu",
                options
            );


            switch (selected)
            {
                case 0:
                    exit  = true;
                    break;
                
                
                case 1:

                    QuestionCreator creator = new();

                    creator.Create();

                    break;



                case 2:

                    EditQuestion editQuestion = new();

                    editQuestion.Edit();

                    break;



                case 3:

                    RemoveQuestion removeQuestion = new();

                    removeQuestion.Remove();

                    break;



                case 4:

                    ReviewAnswers reviewAnswers = new();

                    reviewAnswers.Review();

                    break;



                case 5:

                    exit = true;

                    break;
            }
        }


        Console.Clear();


        MainMenu mainMenu = new();

        mainMenu.Show();
    }
}