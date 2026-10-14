using TaskMaster.Domain;

namespace TaskMaster.Application.Abstractions.Persistence;

public interface IAnswerRepository
{
    void Add(Answer answer);
    List<Answer> GetAll(string? username = null);
    void Review(Answer answer, State status);
}
