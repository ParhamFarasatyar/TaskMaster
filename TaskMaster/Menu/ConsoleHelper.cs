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
    
    public static string? ReadInput(string message, bool hideInput = false)
    {
        Console.Write(message);

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