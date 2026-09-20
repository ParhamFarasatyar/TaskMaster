using UserModel;

namespace Menu;

public class DesignerMenu
{
    public void Show(User user)
    {
        while (true)
        {
            string[] options =
            {
                "Profile Info",
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

            if(selected == -1) return;

            switch(selected)
            {
                case 0:
                    Profile profile = new();
                    bool deleted = profile.Show(user);
                    if (deleted) return;
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
                    return;
            }
        }
    }
}