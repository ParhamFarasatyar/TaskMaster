using TaskMaster.Application.Services;
using TaskMaster.Domain;
using TaskMaster.Presentation.Cli.Validation;
namespace TaskMaster.Presentation.Cli.Menus;

public class EditQuestion
{
    private readonly QuestionService _questionsService;

    public EditQuestion(QuestionService questionsService)
    {
        _questionsService = questionsService;
    }

    public void Edit()
    {
        Console.Clear();
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        ConsoleHelper.PrintColorizeMessage("    EDIT QUESTION   ", ConsoleColor.White);
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);

        // ---------- Selecting Question ----------

        List<Question> questionList = _questionsService.GetAll();
        string[] questions = QuestionFormatter.Format(questionList);
        List<int> questionIds = questionList.Select(question => question.Id).ToList();
        if (questions.Length == 0)
        {
            ConsoleHelper.ReadInput(
                "No questions available. Press Enter: "
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
        int questionId = questionIds[selected];

        // ---------- Selecting Field ----------

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

        // ---------- Geting Value ----------

        switch (fieldSelected)
        {
            case 0:
                string description;
                while (true)
                {
                    string? descriptionInput = ConsoleHelper.ReadInput(
                        "New Description: "
                    );
                    if (descriptionInput == null)
                    {
                        
                        return;
                    }
                    if (InputValidator.StringValidationInput(
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
                _questionsService.Edit(questionId, "Description", description);
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
                    return;
                }
                difficulty = difficultySelected switch
                {
                    0 => Difficulty.Beginner,
                    1 => Difficulty.MidLevel,
                    2 => Difficulty.Advanced,
                    _ => Difficulty.Beginner
                };
                _questionsService.Edit(questionId, "Difficulty", difficulty);
                break;
            case 2:
                int grade;
                while (true)
                {
                    string? gradeInput = ConsoleHelper.ReadInput(
                        "New Grade: "
                    );
                    if (gradeInput == null)
                    {
                        return;
                    }
                    if (InputValidator.Grade(
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
                _questionsService.Edit(questionId, "Grade", grade);
                break;
        }
        Console.WriteLine();
        ConsoleHelper.PrintColorizeMessage(
            "Question Updated Successfully!",
            ConsoleColor.Green
        );
        ConsoleHelper.ReadInput(
            "Press Enter to return: "
        );
    }
}