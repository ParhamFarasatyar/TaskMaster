using DataBase;
using System.Text.Json.Serialization;
namespace TaskMaster.DataModel;

using DataBase.Question;

public enum Difficulty { Beginner, MidLevel, Advanced }

public class Question
{
    public int Id { get; private set; }
    public string? Description { get; private set; }
    public int Grade { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }
    private static List<Question> GetQuestions() => Database.Load<Question>(DataType.Questions);

    public Question(string description, int grade, Difficulty difficulty)
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Description = description;
        Grade = grade;
        Difficulty = difficulty;
    }
    public Question(string description, int grade, Difficulty difficulty, int id)
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Description = description;
        Grade = grade;
        Difficulty = difficulty;
        Id = id;
    }
    public static string[] FormatQuestions(List<Question> questions)
    {
        string[] formattedQuestions = new string[questions.Count];

        for (int i = 0; i < questions.Count; i++)
        {
            string? description = questions[i].Description;
            if (description?.Length > 20)
            {
                description = description[..20] + "...";
            }

            formattedQuestions[i] = $"""
        ┌─────────────────────────────
          │ Task
          ├─────────────────────────────
          │ Description : {description}
          │ Difficulty  : {questions[i].Difficulty}
          │ Score       : {questions[i].Grade}
          └─────────────────────────────
        """;
        }

        return formattedQuestions;
    }
}
