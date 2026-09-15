using QuestionDatatype;
using DataBase;
namespace TaskMaster;

class Program
{
    static void Main(string[] args)
    {
        Question question = new Question("find the max num of array", 2, Difficulty.Beginner);
        question.Add();
    }
}