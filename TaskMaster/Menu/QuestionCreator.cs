using QuestionDatatype;
namespace Menu;

public class QuestionCreator
{
    public void Create()
    {
        Console.Clear();

        Console.WriteLine("====================");
        Console.WriteLine("   CREATE QUESTION   ");
        Console.WriteLine("====================");


        Console.Write("Enter Question Description: ");
        string description = Console.ReadLine()!;


        Console.Write("Enter Question Grade: ");
        int grade = int.Parse(Console.ReadLine()!);


        Console.WriteLine();


        string[] difficultyOptions =
        {
            "Beginner",
            "MidLevel",
            "Advanced"
        };


        int selectedDifficulty = ConsoleMenu.Show(
            "Select Difficulty",
            difficultyOptions
        );


        Difficulty difficulty = selectedDifficulty switch
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
        Console.WriteLine();
        Console.WriteLine("Question Created Successfully!");

        Console.WriteLine(question);


        Console.WriteLine("\nPress Enter to return...");
        Console.ReadLine();
    }
}