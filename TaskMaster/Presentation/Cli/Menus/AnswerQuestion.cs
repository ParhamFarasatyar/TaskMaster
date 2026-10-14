using TaskMaster.Application.Services;
using TaskMaster.Domain;
using TaskMaster.Presentation.Cli.Validation;

namespace TaskMaster.Presentation.Cli.Menus;

public class AnswerQuestion
{
    private readonly QuestionService _questions;
    private readonly AnswerService _answers;

    public AnswerQuestion(QuestionService questions, AnswerService answers)
    {
        _questions = questions;
        _answers = answers;
    }

    public void SubmitAnswer(string userName, Level level)
    {
        List<Question> availableQuestions = _questions.GetAvailable((Difficulty)level, userName);
        string[] savedQuestions = QuestionFormatter.Format(availableQuestions);

        if (savedQuestions.Length == 0)
        {
            ConsoleHelper.PrintColorizeMessage(
                "No questions available.",
                ConsoleColor.Yellow
            );
            ConsoleHelper.Countdown();
            return;
        }

        int selectedIndex = ConsoleMenu.Show("Questions", savedQuestions);

        if (selectedIndex == -1) return;

        string? code = GetCode();
        if (code == null) return;

        Question selectedQuestion = availableQuestions[selectedIndex];
        Answer answer = new(userName, selectedQuestion.Id, code, selectedQuestion.Grade, selectedQuestion.Description);

        _answers.Submit(answer);
        ConsoleHelper.PrintColorizeMessage("Answer submitted successfully.", ConsoleColor.Green);
        ConsoleHelper.Countdown();
    }


    private string? GetCode()
    {
        while (true)
        {
            string? code = ConsoleHelper.ReadInput("Code: ");
            if (code == null) return null;
            if (InputValidator.StringValidationInput(code, out string? errorMessage))
                return code;
            ConsoleHelper.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
        }
    }
}
