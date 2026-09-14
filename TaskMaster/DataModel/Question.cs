using System.Security.Cryptography.X509Certificates;
using DataBase;
using UserModel;
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
    public bool Edit()
    {
        List<Question> questions = Database.Load<Question>( DataType.Questions);
        // return true;
        Question? question = questions.FirstOrDefault(t => t.Id == this.Id);
            Console.WriteLine($"ID entered: {this.Id}");
        //---------- Input validating ----------
        foreach (Question item in questions)
            {
                Console.WriteLine($"ID in JSON: {item.Id}");
            }

            if (question == null)
            {
                Console.WriteLine("Task not found.");
                return false;
            }
        return true;
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
