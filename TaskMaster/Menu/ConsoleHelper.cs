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
    
    public static string? ReadInput(string message)
    {
        Console.Write(message);
        
        string input = "";
        
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            
            // B = Back
            // فقط زمانی که اولین کاراکتر ورودی باشد
            if ((key.KeyChar == 'b' || key.KeyChar == 'B')
                && input.Length == 0)
            {
                Console.WriteLine();

                return null;
            }
            
            // Enter = Finish
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                return input;
            }
            
            // Backspace
            if (key.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input[..^1];

                    Console.Write("\b \b");
                }

                continue;
            }
            
            input += key.KeyChar;

            Console.Write(key.KeyChar);
        }
    }
}