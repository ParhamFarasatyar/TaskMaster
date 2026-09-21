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

        if (savedAnswers.Length == 0)
        {
            Console.WriteLine("No answers available.");
            Thread.Sleep(2000);
            return;
        }

        int selectedIndex = ConsoleMenu.Show("Answer Status", savedAnswers);

        if (selectedIndex == -1) return;

        State state = GetState();

        int grade = GetGrade(state, answers[selectedIndex]);
        
        answers[selectedIndex].SetGrade(grade);
        answers[selectedIndex].SetApprovalStatus(state);
        
        Database.Update(answers, DataType.Answers);
    }


    private State GetState()
    {
        int selectedIndex = 0;
        State state = 0;

        string[] options = ["Approve", "Reject"];

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
