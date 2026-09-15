namespace Menu;

public class RemoveQuestion
{
    public void Remove()
    {
        Console.Clear();

        Console.WriteLine("====================");
        Console.WriteLine("   REMOVE QUESTION  ");
        Console.WriteLine("====================");


        // Questions will be loaded from Database
        // by responsible module

        string[] questions = Array.Empty<string>();


        if (questions.Length == 0)
        {
            Console.WriteLine("No questions available.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }



        int selected = ConsoleMenu.Show(
            "Select Question To Remove",
            questions
        );



        Console.Clear();


        Console.WriteLine(
            $"Selected Question: {questions[selected]}"
        );


        string[] confirmOptions =
        {
            "Yes, Remove",
            "No, Cancel"
        };


        int confirm = ConsoleMenu.Show(
            "Are you sure?",
            confirmOptions
        );



        if (confirm == 0)
        {
            // Delete logic will be implemented
            // by Database/Question module

            Console.WriteLine(
                "Question Removed Successfully!"
            );
        }
        else
        {
            Console.WriteLine(
                "Remove Cancelled"
            );
        }



        Console.WriteLine();

        Console.WriteLine(
            "Press Enter to return..."
        );

        Console.ReadLine();
    }
}