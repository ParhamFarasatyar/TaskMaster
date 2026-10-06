using System.Text.Json.Serialization;
using DataBase;
using Menu;
using UserModel;

namespace TaskMaster.DataModel;

public enum State { Pending, Approve, Reject}

public class Answer
{
    public string UserName { get; set; }
    public string AnswerId{ get; init; }
    [JsonInclude]
    public State ApprovalStatus{ get; private set; }
    public DateTime CreatedDate{ get; init; }
    public string QuestionId{ get; private set; }
    public string Code{ get; private set; }
    [JsonInclude]
    public int Grade{ get; private set; }
    public int GoalGrade { get; private set; }


    public Answer(string userName, string questionId, string code, int goalGrade)
    {
        UserName = userName;
        AnswerId = Guid.NewGuid().ToString("N");
        QuestionId = questionId;
        CreatedDate = DateTime.Now;
        Code = code;
        ApprovalStatus = State.Pending;
        Grade = -1;
        GoalGrade = goalGrade;
    }

    
    public void AnswerQuestion()
    {
        Database.Save(this,DataType.Answers);
    }
    
    public void SetApprovalStatus(int state, int answerIndex)
    {
        List<Answer> loadData = Database.Load<Answer>(DataType.Answers);
        loadData[answerIndex].ApprovalStatus = (State)state;
        Database.Update(loadData, DataType.Answers);
    }


    public static string[] ShowAnswers()
    {
        return ShowAnswers(Database.Load<Answer>(DataType.Answers));
    }

    public static string[] ShowAnswers(List<Answer> answers)
    {
        string[] answersArr = new string[answers.Count];

        for (int i = 0; i < answers.Count; i++)
        {
            int score = answers[i].Grade == -1 ? answers[i].Grade + 1 : answers[i].Grade;
            string answerItem = $"""
             ┌────────────────────────────
               │ user name: {answers[i].UserName}
               │ Code: {answers[i].Code}
               │ Date/Time: {answers[i].CreatedDate:dd/MM/yyyy HH:mm}
               │ State: {answers[i].ApprovalStatus}
               │ Score: {score}
               └─────────────────────────────
           """;
            answersArr[i] = answerItem;
        }

        return answersArr;
    }
    
    
    public static string[] ShowAnswers(string userName)
    {
        List<Answer> answers = Database.Load<Answer>(DataType.Answers)
            .Where(answer => answer.UserName == userName)
            .ToList();

        return ShowAnswers(answers);
    }


    public static void AnswerStatus(string answerId)
    {
        List<Answer> answers = Database.Load<Answer>(DataType.Answers);
        Answer? selectedAnswer = answers.FirstOrDefault(answer => answer.AnswerId == answerId);
        if (selectedAnswer is null)
        {
            ConsoleHelper.PrintColorizeMessage(
                "Answer not found.",
                ConsoleColor.Red
            );
            return;
        }

        foreach (Answer ans in new[] { selectedAnswer })
        {
            string answer = $"""
             ┌─────────────────────────────
               │ Answer
               ├─────────────────────────────
               │ code: {ans.Code}
               │ Date/Time: {ans.CreatedDate}
               | Status: {ans.ApprovalStatus}
               | Score: {ans.Grade}
               └─────────────────────────────
             """;
            ConsoleHelper.PrintColorizeMessage(
                answer,
                ConsoleColor.White
            );
        }
    }
    
    public void SetGrade(int grade) => Grade = grade;

    public void SetApprovalStatus(State state) => ApprovalStatus = state;
}