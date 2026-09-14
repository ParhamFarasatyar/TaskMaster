using System.Security.Cryptography.X509Certificates;
using DataBase;
namespace QuestionDatatype;

public enum Difficulty { Beginner, MidLevel, Advanced }

public class Question
{
    public string? Description { get; private set; }
    public int Grade { get; private set; }
    public string? Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get;private set; }
    public Difficulty Difficulty { get; private set;}

    public Question(string description, int grade, Difficulty difficulty )
    {
        Id = Guid.NewGuid().ToString("N");
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Description = description;
        Grade = grade;
        Difficulty = difficulty;
    }
    public bool Add()
    {
        //---------- Input validating ----------
        Database.Save(this, DataType.Questions);
        return true;
    }
    public void Edit()
    {
        //---------- Edit Question ----------
    }
    public void Delete()
    {
        //---------- Delete Question ----------
    }
    public override string ToString()
    {
        return $"""
        ┌─────────────────────────────
        │ Task
        ├─────────────────────────────
        │ Description : {Description}
        │ Difficulty  : {Difficulty}
        │ Score  : {Grade}
        │ ID     : {Id}
        | Created at : {CreatedAt}
        | Updated at : {UpdatedAt}
        └─────────────────────────────
        """;
    }
}
