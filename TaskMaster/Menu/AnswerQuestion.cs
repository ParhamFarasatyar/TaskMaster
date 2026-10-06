using DataBase;
using Menu;
using TaskMaster.DataModel;
using UserModel;

namespace TaskMaster.Menu;

public class AnswerQuestion
{
    public void SubmitAnswer(string userName, Level level)
    {
        List<Question> availableQuestions = Question.GetQuestionsUserCanAnswer((Difficulty)level, userName);
        string[] savedQuestions = Question.FormatQuestions(availableQuestions);

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

        string code = GetCode();
        if (code.Equals("b", StringComparison.OrdinalIgnoreCase)) return;

        Question selectedQuestion = availableQuestions[selectedIndex];
        Answer answer = new Answer(userName, selectedQuestion.Id!, code, selectedQuestion.Grade);

        Database.Save(answer, DataType.Answers);
    }


    private string GetCode()
    {
        bool status = false;
        string code = "";

        while (!status)
        {
            ConsoleHelper.PrintBackHint();
            ConsoleHelper.PrintColorizeMessage("Code:", ConsoleColor.White);
            code = Console.ReadLine()!;

            status = SystemValidation.System.StringValidationInput(code, out string? errorMessage);

            if (status == false)
                SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
        }

        return code;
    }
}
