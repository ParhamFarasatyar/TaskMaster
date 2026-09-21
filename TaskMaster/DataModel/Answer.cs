using DataBase;

namespace TaskMaster.DataModel;

public enum State { Pending, Approve, Reject}

public class Answer
{
    public string UserName { get; set; }
    public string AnswerId{ get; init; }
    public State ApprovalStatus{ get; private set; }
    public DateTime CreatedDate{ get; init; }
    public string QuestionId{ get; private set; }
    public string Code{ get; private set; }
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
        List<Answer> answers = Database.Load<Answer>(DataType.Answers);
        string[] answersArr = new string[answers.Count];

        for (int i = 0; i < answers.Count; i++)
        {
            string answerItem = $"""
             ┌────────────────────────────
             │ Code: {answers[i].Code}
             │ Date/Time: {answers[i].CreatedDate:dd/mm/yyyy HH:mm}
             │ State: {answers[i].ApprovalStatus}
             │ Score: {answers[i].Grade}
             └─────────────────────────────
           """;
            answersArr[i] = answerItem;
        }

        return answersArr;
    }


    public static void AnswerStatus(string answerId)
    {
        List<Answer> answers = Database.Load<Answer>(DataType.Answers);
        foreach (Answer ans in answers)
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
            Console.WriteLine(answer);
        }
    }
    
    public void SetGrade(int grade) => Grade = grade;
}