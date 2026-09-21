using TaskMaster.DataModel;

namespace Menu;

public class RemoveQuestion
{
    public void Remove()
    {
        Console.Clear();

        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        ConsoleHelper.PrintColorizeMessage("   REMOVE QUESTION  ", ConsoleColor.White);
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);


        // Questions will be loaded from Database
        // by responsible module

        string[] questions = Question.MenuQuestions();


        if (questions.Length == 0)
        {
            ConsoleHelper.PrintColorizeMessage(
                "No questions available.",
                ConsoleColor.Yellow
            );
            ConsoleHelper.Countdown();
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


        ConsoleHelper.PrintColorizeMessage(
            $"Selected Question: {questions[selected]}",
            ConsoleColor.White
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

            ConsoleHelper.PrintColorizeMessage(
                "Question Removed Successfully!",
                ConsoleColor.Green
            );
        }
        else
        {
            ConsoleHelper.PrintColorizeMessage(
                "Remove Cancelled",
                ConsoleColor.Yellow
            );
        }



        ConsoleHelper.Countdown();
    }
}