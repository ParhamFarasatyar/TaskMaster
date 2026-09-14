namespace Question;

public enum Difficulty { Beginner, MidLevel, Advanced }

public class Question
{
    public string? Description { get; private set; }
    public int Grade { get; private set; }
    public string? Id { get; init; }
    public string? Hint { get; private set; }    
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get;private set; }
    public Difficulty Difficulty { get; private set;}

    public Question(string description, int grade, string hint, Difficulty difficulty )
    {
        Id = Guid.NewGuid().ToString("N");
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Description = description;
        Grade = grade;
        Hint = hint;
        Difficulty = difficulty;
    }
    public static void Add()
    {
        //---------- Add Question ----------
    }
    public override string ToString()
    {
        return $"""
        ┌─────────────────────────────
        │ Task
        ├─────────────────────────────
        │ Description : {Description}
        │ Hint   : {Hint}
        │ Difficulty  : {Difficulty}
        │ Score  : {Grade}
        │ ID     : {Id}
        | Created at : {CreatedAt}
        | Updated at : {UpdatedAt}
        └─────────────────────────────
        """;
    }
}
