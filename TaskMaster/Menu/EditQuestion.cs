using TaskMaster.DataModel;
namespace Menu;

public class EditQuestion
{
    public void Edit()
    {
        Console.Clear();
        Console.WriteLine("====================");
        Console.WriteLine("    EDIT QUESTION   ");
        Console.WriteLine("====================");
        string[] questions = Question.MenuQuestions();
        if (questions.Length == 0)
        {
            ConsoleHelper.ReadInput(
                "No questions available. Press Enter (B + Enter = Back): "
            );
            return;
        }
        int selected = ConsoleMenu.Show(
            "Select Question",
            questions
        );
        if (selected == -1)
        {
            return;
        }
        SelectField(selected);
    }
    private void SelectField(int selected)
    {
        string[] questionFields =
        {
            "Description",
            "Difficulty",
            "Grade",
            "Back"
        };
        int fieldSelected = ConsoleMenu.Show(
            "Select Field",
            questionFields
        );
        if (fieldSelected == -1 || fieldSelected == 3)
        {
            return;
        }
        EditField(
            selected,
            fieldSelected
        );
    }
    private void EditField(
        int selected,
        int fieldSelected)
    {
        switch (fieldSelected)
        {
            case 0:
                string description;
                while (true)
                {
                    string descriptionInput = ConsoleHelper.ReadInput(
                        "New Description (B + Enter = Back): "
                    )!;
                    if (descriptionInput == null)
                    {
                        SelectField(selected);
                        return;
                    }
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
                Question.Edit(selected, description, fieldSelected);
                break;
            case 1:
                Difficulty difficulty = Difficulty.Beginner;
                string[] difficultyOptions =
                {
                    "Beginner",
                    "MidLevel",
                    "Advanced",
                    "Back"
                };
                int difficultySelected = ConsoleMenu.Show(
                    "Select Difficulty",
                    difficultyOptions
                );
                if (difficultySelected == -1 || difficultySelected == 3)
                {
                    SelectField(selected);
                    return;
                }
                difficulty = difficultySelected switch
                {
                    0 => Difficulty.Beginner,
                    1 => Difficulty.MidLevel,
                    2 => Difficulty.Advanced,
                    _ => Difficulty.Beginner
                };
                Question.Edit(selected, difficulty, fieldSelected);
                break;
            case 2:
            int grade;
                while (true)
                {
                    string? gradeInput = ConsoleHelper.ReadInput(
                        "New Grade (B + Enter = Back): "
                    );
                    if (gradeInput == null)
                    {
                        SelectField(selected);
                        return;
                    }
                    if (SystemValidation.System.Grade(
                        gradeInput,
                        out string? errorMessage))
                    {
                        grade = int.Parse(gradeInput);
                        break;
                    }
                    ConsoleHelper.PrintColorizeMessage(
                        errorMessage!,
                        ConsoleColor.Red
                    );
                }
                Question.Edit(selected, grade, fieldSelected);
                break;
        }
        Console.WriteLine();
        ConsoleHelper.PrintColorizeMessage(
            "Question Updated Successfully!",
            ConsoleColor.Green
        );
        ConsoleHelper.ReadInput(
            "Press Enter to return (B + Enter = Back): "
        );
    }
}