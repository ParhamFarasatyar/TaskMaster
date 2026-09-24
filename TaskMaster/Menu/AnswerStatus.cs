using DataBase;
using Menu;
using TaskMaster.DataModel;
using UserModel;

namespace TaskMaster.Menu;

public class AnswersStatus
{
    public void UpdateAnswerStatus(string userName)
    {
        List<Answer> answers = Database.Load<Answer>(DataType.Answers);

        string[] savedAnswers = Answer.ShowAnswers(userName);

        if (savedAnswers.Length == 0)
        {
            ConsoleHelper.PrintColorizeMessage(
                "No answers available.",
                ConsoleColor.Yellow
            );
            ConsoleHelper.Countdown();
            return;
        }

        
        ShowAnswers(savedAnswers);
        ConsoleHelper.PrintColorizeMessage("Enter any key to continue", ConsoleColor.Yellow);
        Console.ReadKey();
    }

    private void ShowAnswers(string[] answers)
    {
        foreach (string answer in answers)
        {
            Console.WriteLine(answer);
        }
    }
}
