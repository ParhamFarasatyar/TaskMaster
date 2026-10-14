using TaskMaster.Application.Services;

namespace TaskMaster.Presentation.Cli.Menus;

public class AnswersStatus
{
    private readonly AnswerService _answersService;

    public AnswersStatus(AnswerService answersService)
    {
        _answersService = answersService;
    }

    public void UpdateAnswerStatus(string userName)
    {
        string[] savedAnswers = AnswerFormatter.Format(_answersService.GetAll(userName));

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
