namespace SystemValidation;

public static class System
{
    public static bool Grade(string input, out string? errorMessage)
    {
        bool isValid;
        int Input;
        isValid = int.TryParse(input, out Input);
        if (!isValid)
        {
            errorMessage = "Invalid input!\nPlease enter a number.";
            return false;
        }
        errorMessage = null;
        if (
                Input < 1 ||
                Input > 5)
        {
            isValid = false;
            errorMessage = "the grade must be from 1 to 5";
        }

        return isValid;
    }
}