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
            
            // Enter = تایید ورودی
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                
                // فقط اگر کاربر دقیقاً b یا B وارد کرده باشد
                // یعنی Back
                if(input == "b" || input == "B")
                {
                    return null;
                }
                
                return input;
            }
            
            // Backspace
            if (key.Key == ConsoleKey.Backspace)
            {
                if(input.Length > 0)
                {
                    input = input[..^1];

                    Console.Write("\b \b");
                }
                
                continue;
            }
            
            // ذخیره کاراکتر و نمایش روی کنسول
            input += key.KeyChar;

            Console.Write(key.KeyChar);
        }
    }
}