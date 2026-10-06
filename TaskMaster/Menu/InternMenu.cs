using TaskMaster.Menu;
using UserModel;


namespace Menu;

public class InternMenu
{
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
                    Profile profile = new();
                    bool deleted = profile.Show(user);
                    if(deleted) return false;
                    break;

                case 1:
                    AnswerQuestion answerQuestion = new();

                    answerQuestion.SubmitAnswer(user.UserName!, user.Level);

                    break;

                case 2:
                    AnswersStatus answersStatus = new();

                    answersStatus.UpdateAnswerStatus(user.UserName!);

                    break;

                case 3:
                    return true;
            }
        }
    }
}