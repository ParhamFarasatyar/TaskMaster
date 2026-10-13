namespace TaskMaster.Domain;

public enum State { Pending, Approve, Reject}

public class Answer
{
    public int Id { get; private set; }
    public string UserName { get; }
    public State ApprovalStatus{ get; private set; }
    public DateTime CreatedDate{ get; init; }
    public int QuestionId{ get; }
    public string Code{ get; }
    public int Grade{ get; private set; }
    public int GoalGrade { get; private set; }
    public string Description { get; }


    public Answer(string userName, int questionId, string code, int goalGrade, string description)
        : this(userName, questionId, code, goalGrade, description, State.Pending, DateTime.Now, -1, 0)
    {
    }

    public Answer(
        string userName,
        int questionId,
        string code,
        int goalGrade,
        string description,
        State approvalStatus,
        DateTime createdDate,
        int grade,
        int id)
    {
        Id = id;
        UserName = userName;
        QuestionId = questionId;
        CreatedDate = createdDate;
        Code = code;
        ApprovalStatus = approvalStatus;
        Grade = grade;
        GoalGrade = goalGrade;
        Description = description;
    }

    public void ApplyReview(State state)
    {
        if (ApprovalStatus != State.Pending)
        {
            throw new InvalidOperationException("Only pending answers can be reviewed.");
        }
        if (state is not (State.Approve or State.Reject))
        {
            throw new ArgumentOutOfRangeException(nameof(state));
        }

        ApprovalStatus = state;
        Grade = state == State.Approve ? GoalGrade : 0;
    }
}