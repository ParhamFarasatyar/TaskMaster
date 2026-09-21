using DataBase;
using Menu;
using TaskMaster.DataModel;

namespace TaskMaster.Menu;

public class AnswersStatus
{
    public void UpdateAnswerStatus()
    {
        List<Answer> answers = Database.Load<Answer>(DataType.Answers);

        string[] savedAnswers = Answer.ShowAnswers();

        int selectedIndex = ConsoleMenu.Show("Answer Status", savedAnswers);

        State state = GetState();

        int grade = GetGrade(state, answers[selectedIndex]);
        
        answers[selectedIndex].SetGrade(grade);
        
        Database.Update(answers, DataType.Answers);
    }


    private State GetState()
    {
        int selectedIndex = 0;
        State state = 0;

        string[] options = { "Approve", "Reject" };

        Console.WriteLine("State: ");
        selectedIndex = ConsoleMenu.Show("Select State", options);
        state = (State)(selectedIndex + 1);

        return state;
    }


    private int GetGrade(State state, Answer answer)
    {
        int grade = state == State.Approve ? answer.GoalGrade : 0;
        
        return grade;
    }
}
