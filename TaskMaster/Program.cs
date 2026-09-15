using QuestionDatatype;
using DataBase;
namespace TaskMaster;

class Program
{
    static void Main(string[] args)
    {
        Question question = new Question("test", 0, Difficulty.Advanced);
        Question question1 = new Question("test", 0, Difficulty.Beginner);
        Question.Delete(0);
    }
}