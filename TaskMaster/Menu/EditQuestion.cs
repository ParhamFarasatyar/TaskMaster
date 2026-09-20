using TaskMaster.DataModel;

namespace Menu;

public class EditQuestion
{
    public void Edit()
    {
        Console.Clear();

        Console.WriteLine("====================");
        Console.WriteLine("    EDIT QUESTION   ");
        Console.WriteLine("====================");


        string[] questions = Question.MenuQuestions();


        if (questions.Length == 0)
        {
            ConsoleHelper.ReadInput(
                "No questions available. Press Enter (F2 = Back): "
            );

            return;
        }



        int selected = ConsoleMenu.Show(
            "Select Question",
            questions
        );


        if (selected == -1)
        {
            return;
        }



        SelectField(selected);
    }



    private void SelectField(int selected)
    {
        string[] questionFields =
        {
            "Description",
            "Difficulty",
            "Grade",
            "Back"
        };


        int fieldSelected = ConsoleMenu.Show(
            "Select Field",
            questionFields
        );


        if(fieldSelected == -1)
        {
            return;
        }



        if(fieldSelected == 3)
        {
            return;
        }



        EditField(
            selected,
            fieldSelected
        );
    }




    private void EditField(
        int selected,
        int fieldSelected)
    {
        Difficulty difficulty = Difficulty.Beginner;

        string description = "";

        string grade = "-1";



        switch(fieldSelected)
        {

            case 0:

                string? descriptionInput;


                while(true)
                {
                    descriptionInput = ConsoleHelper.ReadInput(
                        "New Description (F2 = Back): "
                    );


                    if(descriptionInput == null)
                    {
                        SelectField(selected);
                        return;
                    }


                    if(SystemValidation.System.StringValidationInput(
                        descriptionInput,
                        out string? errorMessage))
                    {
                        description = descriptionInput;
                        break;
                    }



                    ConsoleHelper.PrintColorizeMessage(
                        errorMessage!,
                        ConsoleColor.Red
                    );
                }
                Question.Edit(selected, description, fieldSelected);
                break;



            case 1:

                string[] difficultyOptions =
                {
                    "Beginner",
                    "MidLevel",
                    "Advanced",
                    "Back"
                };
                int difficultySelected = ConsoleMenu.Show(
                    "Select Difficulty",
                    difficultyOptions
                );


                if(difficultySelected == -1)
                {
                    SelectField(selected);
                    return;
                }


                if(difficultySelected == 3)
                {
                    SelectField(selected);
                    return;
                }


                difficulty = difficultySelected switch
                {
                    0 => Difficulty.Beginner,
                    1 => Difficulty.MidLevel,
                    2 => Difficulty.Advanced,
                    _ => Difficulty.Beginner
                };


                Question.Edit(
                    selected,
                    difficulty,
                    fieldSelected
                );
                break;



            case 2:

                while(true)
                {
                    string? gradeInput = ConsoleHelper.ReadInput(
                        "New Grade (F2 = Back): "
                    );


                    if(gradeInput == null)
                    {
                        SelectField(selected);
                        return;
                    }



                    if(SystemValidation.System.Grade(
                        gradeInput,
                        out string? errorMessage))
                    {
                        grade = gradeInput;
                        break;
                    }



                    ConsoleHelper.PrintColorizeMessage(
                        errorMessage!,
                        ConsoleColor.Red
                    );
                }
                Question.Edit(
                    selected,
                    int.Parse(grade),
                    fieldSelected
                );
                break;
        }

        Console.WriteLine();

        ConsoleHelper.PrintColorizeMessage(
            "Question Updated Successfully!",
            ConsoleColor.Green
        );


        ConsoleHelper.ReadInput(
            "Press Enter to return (F2 = Back): "
        );
    }
}