using DataBase;
using Menu;
using TaskMaster.DataModel;

namespace TaskMaster.Menu;

public class AnswersStatus
{
    public void UpdateAnswerStatus()
    {
        List<Answer> answers = Database.Load<Answer>(DataType.Answers);

        string[] savedAnswers = Answer.ShowAnswers();

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
