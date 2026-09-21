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
            Console.WriteLine("No questions available.");
            Thread.Sleep(2000);
            return;
        }

        int selectedIndex = ConsoleMenu.Show("Questions", savedQuestions);

        if (selectedIndex == -1) return;

        string code = GetCode();

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
            Console.WriteLine("Code: ");
            code = Console.ReadLine()!;

            status = SystemValidation.System.StringValidationInput(code, out string? errorMessage);

            if (status == false)
                SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
        }

        return code;
    }
}
