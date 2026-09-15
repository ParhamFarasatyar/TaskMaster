using Answer;

namespace SystemValidation;

public static class System
{
    static bool StringValidationInput(string? input, out string? errorMassage)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            errorMassage = "Invalid input!\nEnter something GENIUS!!";
            return false;
        }

        errorMassage = null;
        return true;
    }
    
    
    static bool StateValidationInput(int state, out string? errorMassage)
    {
        if ((State)state != State.Pending || (State)state != State.Approve || (State)state != State.Reject)
        {
            errorMassage = "Invalid input!\nEnter Pending, Approve or Reject.";
            return false;
        }

        errorMassage = null;
        return true;
    }
    public static bool Grade(string input, out string? errorMessage)
    {
        int Input;
        bool isValid = int.TryParse(input, out Input);
        if (!isValid)
        {
            errorMessage = "Invalid input!\nPlease enter a number.";
            return false;
        }
        if (
                Input < 1 ||
                Input > 5)
        {
            isValid = false;
            errorMessage = "the grade must be from 1 to 5";
        }

        errorMessage = null;
        return isValid;
    }
}