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
    public static void Edit(string Id, Question values, int data)
    {
        List<Question> questions = Database.Load<Question>(DataType.Questions);

        Question? question = questions.FirstOrDefault(t => t.Id == Id);

        int index = questions.IndexOf(question!);

        Question newquestion = new Question("", 0, Difficulty.Beginner);
        newquestion = question!;
        Console.WriteLine(question);
        Console.WriteLine(newquestion);
        switch (data)
        {
            case 1:
                newquestion.Description = values.Description;
                break;
            case 2:
                newquestion.Grade = values.Grade;
                break;
            case 3:
                newquestion.Difficulty = values.Difficulty;
                break;

        }
        newquestion.UpdatedAt = DateTime.Now;
        questions[index] = newquestion;
        DataBase.Database.Update(questions, DataType.Questions);
    }
    public static void Delete(int array)
    {
        List<Question> questions = Database.Load<Question>(DataType.Questions);

        Question? question = questions[array];

        questions.Remove(question!);

        Database.Update(questions, DataType.Questions);
    }
    public static void ShowQuestions()
    {
       List<Question> questions = Database.Load<Question>(DataType.Questions);
       foreach(Question question in questions)
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
