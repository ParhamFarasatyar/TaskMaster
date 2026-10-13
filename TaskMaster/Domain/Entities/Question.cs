namespace TaskMaster.Domain;

public enum Difficulty { Beginner, MidLevel, Advanced }

public class Question
{
    public int Id { get; private set; }
    public string Description { get; private set; }
    public int Grade { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }

    public Question(string description, int grade, Difficulty difficulty)
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Description = description;
        Grade = grade;
        Difficulty = difficulty;
    }
    public Question(string description, int grade, Difficulty difficulty, int id)
        : this(description, grade, difficulty, id, DateTime.Now, DateTime.Now)
    {
    }

    public Question(
        string description,
        int grade,
        Difficulty difficulty,
        int id,
        DateTime createdAt,
        DateTime updatedAt)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Description = description;
        Grade = grade;
        Difficulty = difficulty;
        Id = id;
    }

}
