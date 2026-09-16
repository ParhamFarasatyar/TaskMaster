using System;
using DataBase;

namespace AnswerDataModel;

public enum State { Pending, Approve, Reject}

public class Answer
{
    public string AnswerId{ get; init; }
    public State ApprovalStatus{ get; private set; }
    public DateTime CreatedDate{ get; init; }
    public string QuestionId{ get; private set; }
    public string Code{ get; private set; }
    public int Grade{ get; private set; }


    public Answer(string answerId, State approvalStatus, DateTime createdDate, string questionId, string code, int grade)
    {
        AnswerId = answerId == null ? Guid.NewGuid().ToString("N") : answerId;
        QuestionId = questionId;

        if (createdDate == null)
        {
            DateTime utcNow = DateTime.UtcNow;

            TimeZoneInfo tehranZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        
            DateTime CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(utcNow, tehranZone);
        }
        else
        {
            CreatedDate = createdDate;
        }
        
        Code = code;
        ApprovalStatus = approvalStatus == null ? State.Pending : approvalStatus;
    }

    
    public void AnswerQuestion()
    {
        Database.Save(this,DataType.Answers);
    }
    
    public void SetApprovalStatus(int state, string answerId)
    {
        List<Answer> loadData = Database.Load<Answer>(DataType.Answers);
        foreach (Answer answer in loadData)
        {
            if (answer.AnswerId == answerId)
            {
                answer.ApprovalStatus = (State)state;
                Database.Save(answer,DataType.Answers);
            }
        }
    }
}