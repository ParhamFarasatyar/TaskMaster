using TaskMaster.Application.Services;
using TaskMaster.Domain;


namespace TaskMaster.Presentation.Cli.Menus;

public class InternMenu
{
    private readonly QuestionService _questions;
    private readonly AnswerService _answers;
    private readonly UserService _users;

    public InternMenu(QuestionService questions, AnswerService answers, UserService users)
    {
        _questions = questions;
        _answers = answers;
        _users = users;
    }

    public bool Show(User user)
    {
        while(true)
        {
            string[] options =
            {
                "Profile Info",
                "Answer Question",
                "Answers Status",
                "Logout"
            };


            int selected = ConsoleMenu.Show("Intern Menu", options);

            if(selected == -1) return true;

            switch(selected)
            {
                case 0:
                    Profile profile = new(_users);
                    bool deleted = profile.Show(user);
                    if(deleted) return false;
                    break;

                case 1:
                    AnswerQuestion answerQuestion = new(_questions, _answers);

                    answerQuestion.SubmitAnswer(user.UserName!, user.Level);

                    break;

                case 2:
                    AnswersStatus answersStatus = new(_answers);

                    answersStatus.UpdateAnswerStatus(user.UserName!);

                    break;

                case 3:
                    return true;
            }
        }
    }
}