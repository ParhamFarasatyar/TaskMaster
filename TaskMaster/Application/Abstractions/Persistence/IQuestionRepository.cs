using TaskMaster.Domain;

namespace TaskMaster.Application.Abstractions.Persistence;

public interface IQuestionRepository
{
    void Add(Question question);
    List<Question> GetAll();
    List<Question> GetAvailable(Difficulty difficulty, string username);
    int Update(int id, string field, object value);
    bool Delete(int id);
}
