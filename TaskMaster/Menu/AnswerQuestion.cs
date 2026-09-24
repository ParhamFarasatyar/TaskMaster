using DataBase;
using Menu;
using TaskMaster.DataModel;

namespace TaskMaster.Menu;

public class AnswerQuestion
{
    public void SubmitAnswer(string userName)
    {
        string[] savedQuestions = Question.MenuQuestions();

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

        List<Question> questions = Database.Load<Question>(DataType.Questions);

        Answer answer = new Answer(userName, questions[selectedIndex].Id!, code, questions[selectedIndex].Grade);

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
