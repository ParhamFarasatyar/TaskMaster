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


        // Questions will be loaded from Database by responsible module
        // Temporary empty list

        string[] questions = Question.MenuQuestions();

        if (questions.Length == 0)
        {
            Console.WriteLine("No questions available.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        int selected = ConsoleMenu.Show(
            "Select Question",
            questions
        );



        Console.Clear();


        Console.WriteLine(
            $"Editing Question: {questions[selected]}"
        );
        string[] questionfields = { "Description", "Difficulty", "Grade" };

        int fieldSelected = ConsoleMenu.Show("Select field", questionfields);

        Difficulty difficulty = Difficulty.Beginner;
        string description = "";
        string grade = "-1";

        switch (fieldSelected)
        {
            case 0:
                Console.Write("New Description: ");
                description = Console.ReadLine()!;
                while (!SystemValidation.System.StringValidationInput(description, out string? ErrorMesege))
                {
                    SystemValidation.System.PrintColorizeMessage(ErrorMesege!, ConsoleColor.Red);
                    Console.Write("Enter Question Description: ");
                    description = Console.ReadLine()!;
                }
                break;
            case 1:
                string[] difficultyOptions =
                {
                "Beginner",
                "MidLevel",
                "Advanced"
                };


                int difficultySelected = ConsoleMenu.Show(
                    "Select Difficulty",
                    difficultyOptions
                );
                switch (difficultySelected)
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
                }
                break;
            case 2:
                Console.Write("New Grade: ");
                grade = Console.ReadLine()!;
                while (!SystemValidation.System.Grade(grade, out string? ErrorMesege))
                {
                    SystemValidation.System.PrintColorizeMessage(ErrorMesege!, ConsoleColor.Red);
                    Console.Write("Enter Question Grade: ");
                    grade = Console.ReadLine()!;
                }
                break;
        }



        Question newquestion = new Question(description, int.Parse(grade), difficulty);

        Question.Edit(selected, newquestion, fieldSelected);
        Console.WriteLine();

        Console.WriteLine(
            "Question Updated Successfully!"
        );


        Console.WriteLine(
            "Press Enter to return..."
        );

        Console.ReadLine();
    }
}