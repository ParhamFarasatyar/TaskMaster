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
    public DateTime UpdatedAt { get; private set; }
    public Difficulty Difficulty { get; private set; }

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
        //---------- Input validating ----------
        Database.Save(this, DataType.Questions);
    }
    public static void Edit(int index, Question values, int data)
    {
        List<Question> questions = Database.Load<Question>(DataType.Questions);

        Question question = questions[index];
        switch (data)
        {
            case 0:
                question.Description = values.Description;
                break;
            case 1:
                question.Difficulty = values.Difficulty;
                break;
            case 2:
                question.Grade = values.Grade;
                break;

        }
        question.UpdatedAt = DateTime.Now;
        DataBase.Database.Update(questions, DataType.Questions);
    }
    public static void Delete(int index)
    {
        List<Question> questions = Database.Load<Question>(DataType.Questions);

        Question? question = questions[index];

        questions.Remove(question!);

        Database.Update(questions, DataType.Questions);
    }
    public static void ShowQuestions()
    {
        List<Question> questions = Database.Load<Question>(DataType.Questions);
        foreach (Question question in questions)
        {
            string log = $"""
        ┌─────────────────────────────
        │ Task
        ├─────────────────────────────
        │ Description : {question.Description}
        │ Difficulty  : {question.Difficulty}
        │ Score  : {question.Grade}
        | Created at : {question.CreatedAt}
        | Updated at : {question.UpdatedAt}
        └─────────────────────────────
        """;
            Console.WriteLine(log);
        }
    }
    public static string[] MenuQuestions()
    {
        List<string> stringifiedQuestion = new();
        //---------- stringify questions ----------
        List<Question> questions = Database.Load<Question>(DataType.Questions);
        string[] Questions = new string[questions.Count];
        for (int i = 0; i < questions.Count; i++)
        {
            string? description = questions[i].Description?.Length > 20 ? 
            questions[i].Description?[..20] + "..." : questions[i].Description;
            string questionItem = $"""
        ┌────────────────────────────
          │ Description : {description}
          │ Difficulty  : {questions[i].Difficulty}
          │ Score  : {questions[i].Grade}
          └─────────────────────────────
        """;
            Questions[i] = questionItem;
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
        │ Score  : {Grade}
        │ ID     : {Id}
        | Created at : {CreatedAt:dd/mm/yyyy HH:mm}
        | Updated at : {UpdatedAt:dd/mm/yyyy HH:mm}
        └─────────────────────────────
        """;
    }
}
