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
    public static bool ValidateUsername(string? input, out string? errorMessage)
    {
        bool isValid = StringValidationInput(input, out errorMessage);
        if (!isValid) return false;
        if (input!.Length < 5)
        {
            errorMessage = "Invalid input!\nYour username is too short.";
            return false;
        }
        if (input.Length > 20)
        {
            errorMessage = "Invalid input\nYour username is too long.";
            return false;
        }
        if (input.All(c => c == input[0]))
        {
            errorMessage = "Invalid input!\nYour username contain repeated characters";
            return false;
        }
        if (input.Any(c => !char.IsLetterOrDigit(c)))
        {
            errorMessage = "Invalid input!\nOnly use letters and numbers.";
            return false;
        }
        return true;
    }
}