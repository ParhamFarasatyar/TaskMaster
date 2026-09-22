namespace Menu;

public static class ConsoleHelper
{
    public static void PrintBackHint()
    {
        PrintColorizeMessage(
            "Hint: Press B, then Enter to go back.",
            ConsoleColor.DarkGray
        );
    }

    public static void Countdown(int seconds = 3)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;

        for (int remaining = seconds; remaining > 0; remaining--)
        {
            Console.Write($"\rReturning in {remaining}... ");
            Thread.Sleep(1000);
        }

        Console.WriteLine("\rReturning now...       ");
        Console.ResetColor();
    }

    public static void PrintColorizeMessage(
        string message,
        ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static string? ReadInput(string message, bool hideInput = false)
    {
        PrintBackHint();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(message);
        Console.ResetColor();

        string input = "";

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if(key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                
                if(input == "b" || input == "B")
                {
                    return null;
                }
                
                return input;
            }
            
            if(key.Key == ConsoleKey.Backspace)
            {
                if(input.Length > 0)
                {
                    input = input[..^1];

                    Console.Write("\b \b");
                }

                continue;
            }
            
            input += key.KeyChar;

            Console.Write(hideInput ? '*' : key.KeyChar);
        }
    }
}