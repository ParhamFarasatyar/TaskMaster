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
}