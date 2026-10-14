using TaskMaster.Application.Services;

namespace TaskMaster.Presentation.Cli.Menus;

public class RemoveQuestion
{
    private readonly QuestionService _questionsService;

    public RemoveQuestion(QuestionService questionsService)
    {
        _questionsService = questionsService;
    }

    public void Remove()
    {
        Console.Clear();

        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        ConsoleHelper.PrintColorizeMessage("   REMOVE QUESTION  ", ConsoleColor.White);
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);


        List<TaskMaster.Domain.Question> questionList = _questionsService.GetAll();
        List<int> questionIds = questionList.Select(question => question.Id).ToList();
        string[] questions = QuestionFormatter.Format(questionList);


        if (questions.Length == 0)
        {
            ConsoleHelper.PrintColorizeMessage(
                "No questions available.",
                ConsoleColor.Yellow
            );
            ConsoleHelper.Countdown();
            return;
        }



        int selected = ConsoleMenu.Show(
            "Select Question To Remove",
            questions
        );

        if (selected == -1)
        {
            return;
        }



        Console.Clear();


        ConsoleHelper.PrintColorizeMessage(
            $"Selected Question: {questions[selected]}",
            ConsoleColor.White
        );


        string[] confirmOptions =
        {
            "Yes, Remove",
            "No, Cancel"
        };


        int confirm = ConsoleMenu.Show(
            "Are you sure?",
            confirmOptions
        );



        if (confirm == 0)
        {
            _questionsService.Delete(questionIds[selected]);

            ConsoleHelper.PrintColorizeMessage(
                "Question Removed Successfully!",
                ConsoleColor.Green
            );
        }
        else
        {
            ConsoleHelper.PrintColorizeMessage(
                "Remove Cancelled",
                ConsoleColor.Yellow
            );
        }



        ConsoleHelper.Countdown();
    }
}