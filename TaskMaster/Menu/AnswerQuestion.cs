using TaskMaster.DataModel;

namespace Menu;

public class AnswerQuestion
{

    public string GetQuestionId()
    {
        bool status = false;
        string questionId = "";
        
        while (status)
        {
            Console.WriteLine("QuestionId: ");
            questionId = Console.ReadLine();

            status = SystemValidation.System.ValidateUsername(questionId, out string? errorMessage);

            if (status == false)
            {
                SystemValidation.System.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
            }
        }

        return questionId;
    }


    public string GetCode()
    {
        bool status = false;
        string code = "";
        
        while (status)
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