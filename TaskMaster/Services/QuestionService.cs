using DataBase.Question;
using TaskMaster.DataModel;

namespace TaskMaster.Services;

class QuestionService
{
    public static void Add(Question question)
    {
        QuestionDatabase.CreateQuestion(question);
    }
    public static void Edit<T>(int id, T value, int selectedfield)
    {
        QuestionDatabase.UpdateQuestion(id, value, selectedfield);
    }
    public static void Delete(int id)
    {
        QuestionDatabase.DeleteQuestion(id);
    }
    public static string[] MenuQuestions(out List<int> QuestionIds)
    {
        string[] questions = QuestionDatabase.LoadQuestions(out List<int> questionIds);
        QuestionIds = questionIds;
        return questions;
    }
    public static string[] GetQuestionsUserCanAnswer(Difficulty difficulty, string username)
    {
        List<string> Questions = new();
        List<Question> questions = QuestionDatabase.GetQuestionsAsList();
        foreach (Question question in questions)
        {
            if(question.Difficulty != difficulty) continue;
        }
        return Questions.ToArray();
    }
}