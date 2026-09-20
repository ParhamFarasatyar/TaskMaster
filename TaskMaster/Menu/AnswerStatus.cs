using TaskMaster.DataModel;
using DataBase;

namespace Menu;

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
        
        Database.Save(answers[selectedIndex], DataType.Answers);
    }


    public State GetState()
    {
        int selectedIndex = 0;
        State state = 0;

        string[] options =
        {
            "Approve",
            "Reject"
        };

        Console.WriteLine("State: ");
        selectedIndex = ConsoleMenu.Show("Select State", options);
        state = (State)(selectedIndex + 1);

        return state;
    }


    public int GetGrade(State state, Answer answer)
    {
        int grade = 0;
        
        switch (state)
        {
            case State.Approve:
                grade = answer.GoalGrade;
                break;
            case State.Reject:
                grade = 0;
                break;
        }

        return grade;
    }
}
