using QuestionDatatype;

namespace Menu;

public class QuestionCreator
{
    public void Create()
    {
        GetDescription();
    }



    private void GetDescription()
    {
        Console.Clear();

        string? description = ConsoleHelper.ReadInput(
            "Description (F2 = Back): "
        );


        if (description == null)
        {
            return;
        }


        GetGrade(description);
    }



    private void GetGrade(string description)
    {
        Console.Clear();


        string? gradeInput = ConsoleHelper.ReadInput(
            "Grade (F2 = Back): "
        );


        if (gradeInput == null)
        {
            GetDescription();
            return;
        }



        if (!int.TryParse(gradeInput, out int grade))
        {
            ConsoleHelper.PrintColorizeMessage(
                "Invalid Grade",
                ConsoleColor.Red
            );

            Console.ReadLine();

            GetGrade(description);

            return;
        }



        GetDifficulty(description, grade);
    }



    private void GetDifficulty(
        string description,
        int grade)
    {
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



        if(selected == 3)
        {
            GetGrade(description);
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


        question.Add();


        ConsoleHelper.PrintColorizeMessage(
            "Question Created Successfully!",
            ConsoleColor.Green
        );


        Console.ReadLine();
    }
}