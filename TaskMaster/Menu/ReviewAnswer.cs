namespace Menu;
using DataBase;
using TaskMaster.DataModel;

public class ReviewAnswers
{
    public void Review()
    {
        Console.Clear();
        
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        ConsoleHelper.PrintColorizeMessage("   REVIEW ANSWERS   ", ConsoleColor.White);
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        
        List<Answer> answerList = Database.Load<Answer>(DataType.Answers);
        string[] answers = Answer.ShowAnswers();
        
        if (answers.Length == 0)
        {
            ConsoleHelper.PrintColorizeMessage(
                "No answers available.",
                ConsoleColor.Yellow
            );
            ConsoleHelper.Countdown();
            return;
        }
        
        int selected = ConsoleMenu.Show(
            "Select Answer",
            answers
        );

        if (selected == -1)
        {
            return;
        }
        
        Console.Clear();
        
        ConsoleHelper.PrintColorizeMessage(
            $"Selected Answer: {answers[selected]}",
            ConsoleColor.White
        );
        
        string[] reviewOptions = ["Approve", "Reject", "Change Point", "Back"];

        int action = ConsoleMenu.Show("Review Action", reviewOptions);

        if (action == -1 || action == 3)
        {
            return;
        }
        
        switch(action)
        {
            case 0:
                answerList[selected].SetApprovalStatus(State.Approve);
                answerList[selected].SetGrade(answerList[selected].GoalGrade);
                ConsoleHelper.PrintColorizeMessage("Answer Approved", ConsoleColor.Green);
                break;
            
            case 1:
                answerList[selected].SetApprovalStatus(State.Reject);
                answerList[selected].SetGrade(0);
                ConsoleHelper.PrintColorizeMessage("Answer Rejected", ConsoleColor.Red);
                break;

            case 2:
                string? gradeInput = ConsoleHelper.ReadInput("New grade (1-5, B + Enter = Back): ");
                if (gradeInput == null) return;
                if (!SystemValidation.System.Grade(gradeInput, out string? errorMessage))
                {
                    ConsoleHelper.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
                    ConsoleHelper.Countdown();
                    return;
                }

                answerList[selected].SetApprovalStatus(State.Approve);
                answerList[selected].SetGrade(int.Parse(gradeInput));
                ConsoleHelper.PrintColorizeMessage("Answer grade updated", ConsoleColor.Green);
                break;
        }

        Database.Update(answerList, DataType.Answers);
        
        ConsoleHelper.Countdown();
    }
}