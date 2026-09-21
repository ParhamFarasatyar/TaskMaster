using TaskMaster.DataModel;

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

        string[] questions = Question.MenuQuestions();


        if (questions.Length == 0)
        {
            Console.WriteLine("No questions available.");
            Thread.Sleep(2000);
            return;
        }



        int selected = ConsoleMenu.Show(
            "Select Question To Remove",
            questions
        );

        if (selected == -1)
        {
            return;
        }



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
            Question.Delete(selected);

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



        Thread.Sleep(2000);
    }
}