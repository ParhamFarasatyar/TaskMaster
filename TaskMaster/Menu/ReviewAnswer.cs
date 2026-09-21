namespace Menu;

public class ReviewAnswers
{
    public void Review()
    {
        Console.Clear();


        Console.WriteLine("====================");
        Console.WriteLine("   REVIEW ANSWERS   ");
        Console.WriteLine("====================");

        
        // Answers will be loaded from Database
        // by responsible module

        string[] answers = [];


        if (answers.Length == 0)
        {
            Console.WriteLine("No answers available.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }



        int selected = ConsoleMenu.Show(
            "Select Answer",
            answers
        );



        Console.Clear();


        Console.WriteLine(
            $"Selected Answer: {answers[selected]}"
        );


        string[] reviewOptions =
        {
            "Approve",
            "Reject",
            "Change Point",
            "Back"
        };


        int action = ConsoleMenu.Show(
            "Review Action",
            reviewOptions
        );



        switch(action)
        {
            case 0:

                // Approval logic will be implemented
                // by Answer module

                Console.WriteLine(
                    "Answer Approved"
                );

                break;


            case 1:

                Console.WriteLine(
                    "Answer Rejected"
                );

                break;


            case 2:

                Console.WriteLine(
                    "Change Point Selected"
                );

                break;


            case 3:

                return;
        }


        Console.WriteLine();
        Console.WriteLine("Press Enter to return...");
        Console.ReadLine();
    }
}