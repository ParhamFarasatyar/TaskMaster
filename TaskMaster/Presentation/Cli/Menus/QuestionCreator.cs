using TaskMaster.Application.Services;
using TaskMaster.Domain;
using TaskMaster.Presentation.Cli.Validation;
namespace TaskMaster.Presentation.Cli.Menus;

public class QuestionCreator
{
    private readonly QuestionService _questions;

    public QuestionCreator(QuestionService questions)
    {
        _questions = questions;
    }

    public void Create()
    {
        // ---------- GetDescription ----------
        Console.Clear();
        string? description;
        while (true)
        {
            description = ConsoleHelper.ReadInput("Description: ");
            if (description == null) return;
            if (!InputValidator.StringValidationInput(
                description,
                out string? errorMessage))
            {
                ConsoleHelper.PrintColorizeMessage(
                    errorMessage!,
                    ConsoleColor.Red
                );

                ConsoleHelper.ReadInput("Press Enter to try again: ");
            }
            else
            {
                break;
            }
        }
        // ---------- Get Grade ----------
        Console.Clear();
        string? gradeInput;
        while (true)
        {
            gradeInput = ConsoleHelper.ReadInput("Grade (1-5): ");
            if (gradeInput == null) return;
            if (!InputValidator.Grade(
                gradeInput,
                out string? errorMessage))
            {
                ConsoleHelper.PrintColorizeMessage(
                    errorMessage!,
                    ConsoleColor.Red
                );

                ConsoleHelper.ReadInput("Press Enter to try again: ");
            }
            else
            {
                break;
            }
        }
        int grade = int.Parse(gradeInput);

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
            description,
            grade,
            difficulty
        );

        _questions.Add(question);

        ConsoleHelper.PrintColorizeMessage(
            "Question Created Successfully!",
            ConsoleColor.Green
        );

        ConsoleHelper.ReadInput(
            "Press Enter to continue: "
        );
    }
}