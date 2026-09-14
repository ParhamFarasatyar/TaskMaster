using System;

namespace TaskMaster.DataModel;

public class Answer
{
    public string ApprovalStatus;
    public DateTime CreatedDate;
    public int AnswerId;
    public int QuestionId;
    public string Code;
    public int Point;


    public Answer(int answerId, int questionId, DateTime createdDate, string code, string approvalStatus)
    {
        AnswerId = answerId;
        QuestionId = questionId;
        CreatedDate = createdDate;
        Code = code;
        ApprovalStatus = approvalStatus;
    }


    public void AnswerQuestion()
    {
        /*submit answer in database*/
    }
}