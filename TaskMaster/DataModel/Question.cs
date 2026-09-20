using System.Security.Cryptography.X509Certificates;
using DataBase;
using UserModel;
namespace TaskMaster.DataModel;

public enum Difficulty { Beginner, MidLevel, Advanced }

public class Question
{
    public string? Description { get; private set; }
    public int Grade { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public string? Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }

    public Question(string description, int grade, Difficulty difficulty)
    {
        Id = Guid.NewGuid().ToString("N");
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Description = description;
        Grade = grade;
        Difficulty = difficulty;
    }
    public void Add()
    {
        Database.Save(this, DataType.Questions);
    }
    public static void Edit<T>(int index, T value, int data)
    {
        List<Question> questions = Database.Load<Question>(DataType.Questions);

        Question question = questions[index];
        switch (data)
        {
            case 0:
                question.Description = value!.ToString();
                break;
            case 1:
                question.Difficulty = (Difficulty)(object)value!;
                break;
            case 2:
                question.Grade = Convert.ToInt32(value);
                break;

        }
        question.UpdatedAt = DateTime.Now;
        Database.Update(questions, DataType.Questions);
    }
    public static void Delete(int index)
    {
        List<Question> questions = Database.Load<Question>(DataType.Questions);

        Question? question = questions[index];

        questions.Remove(question!);

        Database.Update(questions, DataType.Questions);
    }
    public static string ShowQuestions(Question question)
    {
        string? description = question.Description?.Length > 20 ?
            question.Description?[..20] + "..." : question.Description;
        string _question = $"""
        ┌─────────────────────────────
        │ Task
        ├─────────────────────────────
        │ Description : {description}
        │ Difficulty  : {question.Difficulty}
        │ Score  : {question.Grade}
        | Created at : {question.CreatedAt}
        | Updated at : {question.UpdatedAt}
        └─────────────────────────────
        """;
        return _question;
    }
    public static string[] MenuQuestions()
    {
        List<Question> questions = Database.Load<Question>(DataType.Questions);
        string[] Questions = new string[questions.Count];
        for (int i = 0; i < questions.Count; i++)
        {
            Questions[i] = ShowQuestions(questions[i]);
        }
        return Questions;
    }
    public override string ToString()
    {
        return $"""
        ┌─────────────────────────────
         │ Task
         ├─────────────────────────────
         │ Description : {Description}
         │ Difficulty  : {Difficulty}
         │ Score       : {Grade}
         │ ID          : {Id}
         | Created at  : {CreatedAt:dd/mm/yyyy HH:mm}
         | Updated at  : {UpdatedAt:dd/mm/yyyy HH:mm}
         └─────────────────────────────
        """;
    }
}
