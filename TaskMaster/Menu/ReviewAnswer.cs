namespace Menu;
using DataBase;
using TaskMaster.DataModel;

public class ReviewAnswers
{
    public void Review()
    {
        Console.Clear();
        
        Console.WriteLine("====================");
        Console.WriteLine("   REVIEW ANSWERS   ");
        Console.WriteLine("====================");
        
        List<Answer> answerList = Database.Load<Answer>(DataType.Answers);
        string[] answers = Answer.ShowAnswers();
        
        if (answers.Length == 0)
        {
            Console.WriteLine("No answers available.");
            Thread.Sleep(2000);
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
        
        Console.WriteLine($"Selected Answer: {answers[selected]}");
        
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
                Console.WriteLine("Answer Approved");
                break;
            
            case 1:
                answerList[selected].SetApprovalStatus(State.Reject);
                answerList[selected].SetGrade(0);
                Console.WriteLine("Answer Rejected");
                break;

            case 2:
                string? gradeInput = ConsoleHelper.ReadInput("New grade (1-5, B + Enter = Back): ");
                if (gradeInput == null) return;
                if (!SystemValidation.System.Grade(gradeInput, out string? errorMessage))
                {
                    ConsoleHelper.PrintColorizeMessage(errorMessage!, ConsoleColor.Red);
                    Thread.Sleep(2000);
                    return;
                }

                answerList[selected].SetApprovalStatus(State.Approve);
                answerList[selected].SetGrade(int.Parse(gradeInput));
                Console.WriteLine("Answer grade updated");
                break;
        }

        Database.Update(answerList, DataType.Answers);
        
        Thread.Sleep(2000);
    }
}