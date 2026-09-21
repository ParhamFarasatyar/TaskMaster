using TaskMaster.Menu;
using UserModel;

namespace Menu;

public class InternMenu
{
    public void Show(User user)
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

            if(selected == -1) return;

            switch(selected)
            {
                case 0:
                    Profile profile = new();
                    bool deleted = profile.Show(user);
                    if(deleted) return;
                    break;

                case 1:
                    AnswerQuestion answerQuestion = new();

                    answerQuestion.SubmitAnswer(user.UserName!);

                    break;

                case 2:
                    AnswersStatus answersStatus = new();

                    answersStatus.UpdateAnswerStatus();

                    break;

                case 3:
                    return;
            }
        }
    }
}