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



            // B + Enter = Back
            if (key.KeyChar == 'b' || key.KeyChar == 'B')
            {
                ConsoleKeyInfo nextKey = Console.ReadKey(true);


                if (nextKey.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();

                    return null;
                }



                // اگر بعد از B چیز دیگری وارد شد،
                // B و آن کاراکتر را به عنوان ورودی عادی ذخیره کن

                input += key.KeyChar;

                Console.Write(key.KeyChar);



                input += nextKey.KeyChar;

                Console.Write(nextKey.KeyChar);


                continue;
            }




            // Enter = Finish
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                break;
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



        return input;
    }
}