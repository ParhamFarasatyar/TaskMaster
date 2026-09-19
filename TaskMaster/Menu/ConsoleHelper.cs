namespace Menu;

public static class ConsoleHelper
{
    public static void PrintColorizeMessage(
        string message,
        ConsoleColor color)
    {
        Console.ForegroundColor = color;

        Console.WriteLine(message);

        Console.ResetColor();
    }
}