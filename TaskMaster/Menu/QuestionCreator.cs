using TaskMaster.DataModel;
namespace Menu;

public class QuestionCreator
{
    public void Create()
    {
        Console.Clear();
        string description;
        int grade;
        while (true)
        {
            string descriptionInput = ConsoleHelper.ReadInput(
                "Description: "
            )!;
            if (SystemValidation.System.StringValidationInput(
                descriptionInput,
                out string? errorMessage))
            {
                description = descriptionInput;
                break;
            }
            ConsoleHelper.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );
        }
        Console.Clear();
        while (true)
        {
            string? gradeInput = ConsoleHelper.ReadInput("Grade: ");
            if (SystemValidation.System.Grade(gradeInput!, out string? errorMessage))
            {
                grade = int.Parse(gradeInput!);
                break;
            }
            ConsoleHelper.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );
        }
        Console.Clear();
        Difficulty difficulty = Difficulty.Beginner;
        string[] options = { "Beginner", "MidLevel", "Advanced", "Back" };
        int selected = ConsoleMenu.Show(
            "Select Difficulty",
            options
        );
        switch (selected)
        {
            case 0:
                difficulty = Difficulty.Beginner;
                break;
            case 1:
                difficulty = Difficulty.MidLevel;
                break;
            case 2:
                difficulty = Difficulty.Advanced;
                break;
            case 3:
                return;
        }
        Question question = new(description, grade, difficulty);
        question.Add();
        ConsoleHelper.PrintColorizeMessage(
                "Question Created Successfully",
                ConsoleColor.Green
            );
        ConsoleHelper.ReadInput(
        "Press Enter to return: "
    );
    }
}