using System;
using DataBase;

namespace Answer;

public enum State { Pending, Approve, Reject}

public class Answer
{
    public string AnswerId{ get; init; }
    public State ApprovalStatus{ get; private set; }
    public DateTime CreatedDate{ get; init; }
    public string QuestionId{ get; private set; }
    public string Code{ get; private set; }
    public int Grade{ get; private set; }


    public Answer(string questionId, string code)
    {
        AnswerId = Guid.NewGuid().ToString("N");
        QuestionId = questionId;
        CreatedDate = DateTime.Now;
        Code = code;
        ApprovalStatus = State.Pending;
    }

    
    public void AnswerQuestion()
    {
        Database.Save(this,DataType.Answers);
    }
    
    public void SetApprovalStatus(int state)
    {
        ApprovalStatus = (State)state;
        Database.Update<Answer>([this],DataType.Answers);
    }
}