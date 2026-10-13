using TaskMaster.DataModel;
using TaskMaster.Services;
namespace Menu;

public class QuestionCreator
{
    public void Create()
    {
        // ---------- GetDescription ----------
        Console.Clear();
        string? description = ConsoleHelper.ReadInput("Description: ");
        while (true)
        {

            if (!SystemValidation.System.StringValidationInput(
                description,
                out string? errorMessage))
            {
                ConsoleHelper.PrintColorizeMessage(
                    errorMessage!,
                    ConsoleColor.Red
                );

                ConsoleHelper.ReadInput(
                    "Press Enter to try again: "
                );
            }
            else
            {
                break;
            }
        }
        // ---------- Get Grade ----------
        Console.Clear();
        string? gradeInput = ConsoleHelper.ReadInput(
                "Grade (1-5): "
            );
        while (true)
        {

            if (!SystemValidation.System.Grade(
                gradeInput!,
                out string? errorMessage))
            {
                ConsoleHelper.PrintColorizeMessage(
                    errorMessage!,
                    ConsoleColor.Red
                );

                ConsoleHelper.ReadInput(
                    "Press Enter to try again: "
                );
            }
            else
            {
                break;
            }
        }
        int grade = int.Parse(gradeInput!);

        // Get Difficulty
        Console.Clear();

        string[] options =
        {
            "Beginner",
            "MidLevel",
            "Advanced",
            "Back"
        };

        int selected = ConsoleMenu.Show(
            "Select Difficulty",
            options
        );

        if (selected == -1 || selected == 3)
        {
            return;
        }

        Difficulty difficulty = selected switch
        {
            0 => Difficulty.Beginner,
            1 => Difficulty.MidLevel,
            2 => Difficulty.Advanced,
            _ => Difficulty.Beginner
        };

        Question question = new Question(
            description!,
            grade,
            difficulty
        );

        QuestionService.Add(question);

        ConsoleHelper.PrintColorizeMessage(
            "Question Created Successfully!",
            ConsoleColor.Green
        );

        ConsoleHelper.ReadInput(
            "Press Enter to continue: "
        );
    }
}