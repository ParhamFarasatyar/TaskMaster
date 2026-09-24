using DataBase;
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
    private static List<Question> GetQuestions() => Database.Load<Question>(DataType.Questions);

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
    public static void Edit<T>(int index, T value, int selectedfield)
    {
        List<Question> questions = GetQuestions();
        Question question = questions[index];
        switch (selectedfield)
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
        List<Question> questions = GetQuestions();

        Question? question = questions[index];

        questions.Remove(question!);

        Database.Update(questions, DataType.Questions);
    }
    public static string[] MenuQuestions()
    {
        List<Question> questions = GetQuestions();


        string[] Questions = new string[questions.Count];
        for (int i = 0; i < questions.Count; i++)
        {
            string? description = questions[i].Description?.Length > 20 ?
            questions[i].Description?[..20] + "..." : questions[i].Description;
            string questionItem = $"""
        ┌─────────────────────────────
          │ Task
          ├─────────────────────────────
          │ Description : {description}
          │ Difficulty  : {questions[i].Difficulty}
          │ Score       : {questions[i].Grade}
          | Created at  : {questions[i].CreatedAt}
          | Updated at  : {questions[i].UpdatedAt}
          └─────────────────────────────
        """;
            Questions[i] = questionItem;
        }
        return Questions;
    }
    public static string[] ValidateIfUserCanAnswer(Difficulty difficulty, string username)
    {
        List<Question> questions = GetQuestions();
        List<Answer> answers = Database.Load<Answer>(DataType.Answers);
        List<string> Questions = new List<string>();
        string[] _questions = new string[Questions.Count];
        for (int i = 0; i < questions.Count; i++)
        {
            bool isAnswered = false;
            foreach (Answer answer in answers)
            {
                isAnswered = answer.QuestionId == questions[i].Id && answer.UserName == username;
                if(isAnswered) questions.Remove(questions[i]);
            }
            string? description = questions[i].Description?.Length > 20 ?
            questions[i].Description?[..20] + "..." : questions[i].Description;
            string questionItem = $"""
        ┌─────────────────────────────
          │ Task
          ├─────────────────────────────
          │ Description : {description}
          │ Difficulty  : {questions[i].Difficulty}
          │ Score       : {questions[i].Grade}
          └─────────────────────────────
        """;
            if (questions[i].Difficulty == difficulty) Questions.Add(questionItem);
        }
        for (int i = 0; i < Questions.Count; i++)
        {
            Questions[i] = $"{i + 1}. {Questions[i]}";
        }
        return Questions.ToArray();
    }
}
