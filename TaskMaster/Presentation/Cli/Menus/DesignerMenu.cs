using TaskMaster.Application.Services;
using TaskMaster.Domain;

namespace TaskMaster.Presentation.Cli.Menus;

public class DesignerMenu
{
    private readonly QuestionService _questions;
    private readonly AnswerService _answers;
    private readonly UserService _users;

    public DesignerMenu(QuestionService questions, AnswerService answers, UserService users)
    {
        _questions = questions;
        _answers = answers;
        _users = users;
    }

    public bool Show(User user)
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

            if(selected == -1) return true;

            switch(selected)
            {
                case 0:
                    Profile profile = new(_users);
                    bool deleted = profile.Show(user);
                    if (deleted) return false;
                    break;

                case 1:
                    QuestionCreator creator = new(_questions);
                    creator.Create();
                    break;

                case 2:
                    EditQuestion editQuestion = new(_questions);
                    editQuestion.Edit();
                    break;

                case 3:
                    RemoveQuestion removeQuestion = new(_questions);
                    removeQuestion.Remove();
                    break;

                case 4:
                    ReviewAnswers reviewAnswers = new(_answers);
                    reviewAnswers.Review();
                    break;

                case 5:                    
                    return true;
            }
        }
    }
}