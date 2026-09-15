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

        string[] questions = Array.Empty<string>();


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


        Console.Write("New Description: ");
        string description = Console.ReadLine()!;


        Console.Write("New Grade: ");
        int grade = int.Parse(Console.ReadLine()!);



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