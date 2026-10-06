namespace Menu;
using DataBase;
using TaskMaster.DataModel;
using UserModel;

public class ReviewAnswers
{
    public void Review()
    {
        Console.Clear();
        
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        ConsoleHelper.PrintColorizeMessage("   REVIEW ANSWERS   ", ConsoleColor.White);
        ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
        
        List<Answer> answerList = Database.Load<Answer>(DataType.Answers);
        List<Answer> pendingAnswers = answerList
            .Where(answer => answer.ApprovalStatus == State.Pending)
            .ToList();
        string[] answers = Answer.ShowAnswers(pendingAnswers);
        
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
        
        string[] reviewOptions = 
        [
            "Approve", 
            "Reject", 
            "Back"
        ];

        int action = ConsoleMenu.Show("Review Action", reviewOptions);

        if (action == -1 || action == 2)
        {
            return;
        }
        
        switch(action)
        {
            case 0:
                pendingAnswers[selected].SetApprovalStatus(State.Approve);
                pendingAnswers[selected].SetGrade(pendingAnswers[selected].GoalGrade);
                User.SetScore(pendingAnswers[selected].UserName, pendingAnswers[selected].GoalGrade);
                User.UpdateLevel(pendingAnswers[selected].UserName!);
                ConsoleHelper.PrintColorizeMessage("Answer Approved", ConsoleColor.Green);
                break;
            
            case 1:
                pendingAnswers[selected].SetApprovalStatus(State.Reject);
                pendingAnswers[selected].SetGrade(0);
                ConsoleHelper.PrintColorizeMessage("Answer Rejected", ConsoleColor.Red);
                break;
        }

        Database.Update(answerList, DataType.Answers);
        
        ConsoleHelper.Countdown();
    }
}