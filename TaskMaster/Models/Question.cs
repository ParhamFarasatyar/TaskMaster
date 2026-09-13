namespace TaskMaster.Model.Question;
using Enum;



public class Question
{
    public string? Description { get; private set; }

    public int Grade { get; private set; }
    public string? Id { get; init; }
    public string? Hint { get; private set; }    
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get;private set; }
    public Dificulty Dificulty { get; private set;}

    public Question(string description, int grade, string hint, Dificulty dificulty )
    {
        Id = Guid.NewGuid().ToString("N");
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        Description = description;
        Grade = grade;
        Hint = hint;
        Dificulty = dificulty;
    }
    public override string ToString()
    {
        return $"""
        ┌─────────────────────────────
        │ Task
        ├─────────────────────────────
        │ Description : {Description}
        │ Hint   : {Hint}
        │ Difficulty  : {Dificulty}
        │ Score  : {Grade}
        │ ID     : {Id}
        | Created at : {CreatedAt}
        | Updated at : {UpdatedAt}
        └─────────────────────────────
        """;
    }
}
