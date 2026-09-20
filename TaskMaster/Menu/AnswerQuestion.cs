using DataBase;
using QuestionDatatype;
using TaskMaster.DataModel;

namespace Menu;

public class AnswerQuestion
{
    public void SubmitAnswer()
    {
        string[] answers = Answer.ShowAnswers();

        int selectedIndex = ConsoleMenu.Show("Answers", answers);

        string code = GetCode();

        List<Question> questions = Database.Load<Question>(DataType.Questions);

        Answer answer = new Answer(questions[selectedIndex].Id!, code, questions[selectedIndex].Grade);

        Database.Save(answer, DataType.Answers);
    }


    public string GetCode()
    {
        bool status = false;
        string code = "";

        while (!status)
        {
            Console.WriteLine("Code: ");
            code = Console.ReadLine();

            status = SystemValidation.System.StringValidationInput(code, out string? errorMessage);

            if (status == false)
            {
                SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
            }
        }

        return code;
    }
}
