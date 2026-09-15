using Answer;

namespace SystemValidation;

public static class System
{
    static bool StringValidationInput(string? input, out string? errorMassage)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            errorMassage = "Enter something GENIUS!!";
            return false;
        }

        errorMassage = null;
        return true;
    }
    
    
    static bool StateValidationInput(int state, out string? errorMassage)
    {
        if ((State)state != State.Pending || (State)state != State.Approve || (State)state != State.Reject)
        {
            errorMassage = "Enter Pending, Approve or Reject.";
            return false;
        }

        errorMassage = null;
        return true;
    }
}