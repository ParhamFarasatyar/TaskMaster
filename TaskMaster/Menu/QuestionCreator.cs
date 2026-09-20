using TaskMaster.DataModel;

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
            "Description (B + Enter = Back): "
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
            "Grade (B + Enter = Back): "
        );


        if (gradeInput == null)
        {
            GetDescription();
            return;
        }



        if (!SystemValidation.System.Grade(
                gradeInput,
                out string? errorMessage))
        {
            ConsoleHelper.PrintColorizeMessage(
                errorMessage!,
                ConsoleColor.Red
            );

            Console.ReadLine();

            GetGrade(description);

            return;
        }


        int grade = int.Parse(gradeInput);



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