using TaskMaster.Application.Abstractions.Persistence;
using TaskMaster.Domain;

namespace TaskMaster.Application.Services;

public sealed class QuestionService
{
    private readonly IQuestionRepository _questions;

    public QuestionService(IQuestionRepository questions)
    {
        _questions = questions;
    }

    public void Add(Question question) => _questions.Add(question);

    public List<Question> GetAll() => _questions.GetAll();

    public List<Question> GetAvailable(Difficulty difficulty, string username) =>
        _questions.GetAvailable(difficulty, username)
            .Where(question => question.Difficulty <= difficulty)
            .ToList();

    public void Edit(int id, string field, object value)
    {
        if (_questions.Update(id, field, value) != 1)
        {
            throw new InvalidOperationException("The question could not be updated because it no longer exists.");
        }
    }

    public void Delete(int id)
    {
        if (!_questions.Delete(id))
        {
            throw new InvalidOperationException("The question could not be deleted because it no longer exists.");
        }
    }
}
