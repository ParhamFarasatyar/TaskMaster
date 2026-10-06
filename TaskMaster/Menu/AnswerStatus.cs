using DataBase;
using Menu;
using TaskMaster.DataModel;
using UserModel;

namespace TaskMaster.Menu;

public class AnswersStatus
{
    public void UpdateAnswerStatus(string userName)
    {
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
        Console.Clear();
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        ConsoleHelper.PrintColorizeMessage("Answers Status", ConsoleColor.White);
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        foreach (string answer in answers)
        {
            Console.WriteLine(answer);
        }
    }
}
