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
    
    
    static bool StateValidationInput(string state, out string? errorMassage)
    {
        if (state.ToLower() != "pending" || state.ToLower() != "approve" || state.ToLower() != "reject")
        {
            errorMassage = "Enter Pending, Approve or Reject.";
            return false;
        }

        errorMassage = null;
        return true;
    }
}