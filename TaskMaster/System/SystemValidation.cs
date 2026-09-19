using Answer;

namespace SystemValidation;

public static class System
{
    public static bool StringValidationInput(string? input, out string? errorMessage)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            errorMessage = "Invalid input!\nEnter something GENIUS!!";
            PrintColorizeMessage(errorMessage, ConsoleColor.Red);
            return false;
        }

        errorMessage = null;
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
            errorMessage = "Invalid input!\nthe grade must be from 1 to 5";
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
    public static bool ValidateUserPassword(string? input, out string? errorMessage)
    {
        bool isValid = StringValidationInput(input, out errorMessage);
        if (!isValid) return false;
        if (input!.Length < 8)
        {
            errorMessage = "Invalid Input!\nYour password must be 8 character at least.";
            return false;
        }
        bool hasDigit = false;
        bool hasUpper = false;
        bool hasLower = false;
        bool hasSpecialCharacter = false;
        foreach (char character in input)
        {
            if (char.IsDigit(character)) hasDigit = true;
            if (char.IsLower(character)) hasLower = true;
            if (char.IsUpper(character)) hasUpper = true;
            if (char.IsWhiteSpace(character))
            {
                errorMessage = "Invalid input!\nWhite space isn't allowed in password.";
                return false;
            }
            if (char.IsPunctuation(character) && !(character == '@'))
            {
                errorMessage = "Invalid input!\nPunctuation isn't allowed in password.(Except '@')";
            }
        }
        if (input.Contains('@') || input.Contains('#') || input.Contains('$')) hasSpecialCharacter = true;

        if (!hasDigit || !hasUpper || !hasLower || !hasSpecialCharacter)
        {
            errorMessage = $"""
            Invalid input!Your password must contain digit,
            lowercase character, uppercase character and special characters(@, #, $).
            """;
            return false;
        }
        return true;
    }
    public static void PrintColorizeMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}