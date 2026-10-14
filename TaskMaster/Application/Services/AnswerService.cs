using TaskMaster.Application.Abstractions.Persistence;
using TaskMaster.Domain;

namespace TaskMaster.Application.Services;

public sealed class AnswerService
{
    private readonly IAnswerRepository _answers;

    public AnswerService(IAnswerRepository answers)
    {
        _answers = answers;
    }

    public void Submit(Answer answer) => _answers.Add(answer);

    public List<Answer> GetAll(string? username = null) => _answers.GetAll(username);

    public void Review(Answer answer, State status)
    {
        if (status is not (State.Approve or State.Reject))
        {
            throw new ArgumentOutOfRangeException(nameof(status), "A review must approve or reject the answer.");
        }

        _answers.Review(answer, status);
        answer.ApplyReview(status);
    }
}
