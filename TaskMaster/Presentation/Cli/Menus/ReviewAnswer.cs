namespace TaskMaster.Presentation.Cli.Menus;
using TaskMaster.Application.Services;
using TaskMaster.Domain;

public class ReviewAnswers
{
    private readonly AnswerService _answersService;

    public ReviewAnswers(AnswerService answersService)
    {
        _answersService = answersService;
    }

    public void Review()
    {
        Console.Clear();
        
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        ConsoleHelper.PrintColorizeMessage("   REVIEW ANSWERS   ", ConsoleColor.White);
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        
        List<Answer> pendingAnswers = _answersService.GetAll()
            .Where(answer => answer.ApprovalStatus == State.Pending)
            .ToList();
        string[] answers = AnswerFormatter.Format(pendingAnswers);
        
        if (answers.Length == 0)
        {
            ConsoleHelper.PrintColorizeMessage(
                "No answers available.",
                ConsoleColor.Yellow
            );
            ConsoleHelper.Countdown();
            return;
        }
        
        int selected = ConsoleMenu.Show(
            "Select Answer",
            answers
        );

        if (selected == -1)
        {
            return;
        }
        
        Console.Clear();
        
        ConsoleHelper.PrintColorizeMessage(
            $"Selected Answer: {answers[selected]}",
            ConsoleColor.White
        );
        
        string[] reviewOptions = 
        [
            "Approve", 
            "Reject", 
            "Back"
        ];

        int action = ConsoleMenu.Show("Review Action", reviewOptions);

        if (action == -1 || action == 2)
        {
            return;
        }
        
        switch(action)
        {
            case 0:
                _answersService.Review(pendingAnswers[selected], State.Approve);
                ConsoleHelper.PrintColorizeMessage("Answer Approved", ConsoleColor.Green);
                break;
            
            case 1:
                _answersService.Review(pendingAnswers[selected], State.Reject);
                ConsoleHelper.PrintColorizeMessage("Answer Rejected", ConsoleColor.Red);
                break;
        }
        
        ConsoleHelper.Countdown();
    }
}