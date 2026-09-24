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
        
        string[] reviewOptions = 
        [
            "Approve", 
            "Reject", 
            "Back"
        ];

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
                User.SetScore(answerList[selected].UserName, answerList[selected].GoalGrade);
                User.UpdateLevel(answerList[selected].UserName!);
                ConsoleHelper.PrintColorizeMessage("Answer Approved", ConsoleColor.Green);
                break;
            
            case 1:
                answerList[selected].SetApprovalStatus(State.Reject);
                answerList[selected].SetGrade(0);
                ConsoleHelper.PrintColorizeMessage("Answer Rejected", ConsoleColor.Red);
                break;
        }

        Database.Update(answerList, DataType.Answers);
        
        ConsoleHelper.Countdown();
    }
}