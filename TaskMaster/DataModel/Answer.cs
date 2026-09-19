using DataBase;

namespace TaskMaster.DataModel;

public enum State { Pending, Approve, Reject}

public class Answer
{
    public string AnswerId{ get; init; }
    public State ApprovalStatus{ get; private set; }
    public DateTime CreatedDate{ get; init; }
    public string QuestionId{ get; private set; }
    public string Code{ get; private set; }
    public int Grade{ get; private set; }


    public Answer(string? answerId, State approvalStatus, string questionId, string code, int grade)
    {
        AnswerId = answerId == null ? Guid.NewGuid().ToString("N") : answerId;
        QuestionId = questionId;

        // List<Answer> answers = new List<Answer>();
        // answers = Database.Load<Answer>(DataType.Answers);
        
        // if (answers.Any(a => a.AnswerId == answerId))
        // {
        //     CreatedDate = createdDate;
        // }
        // else
        // {
        //     DateTime utcNow = DateTime.UtcNow;
        //
        //     TimeZoneInfo tehranZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        //
        //     CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcNow, tehranZone);
        // }
        
        CreatedDate = DateTime.Now;

        
        Code = code;
        ApprovalStatus = approvalStatus == null ? State.Pending : approvalStatus;
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
}