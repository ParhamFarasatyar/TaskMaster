namespace Menu;
public class ConsoleMenu
{
    public static int Show(
        string title,
        string[] options,
        bool allowBack = true,
        bool clearScreen = true)
    {
        if (options.Length == 0)
        {
            return -1;
        }

        int selectedIndex = 0;

        ConsoleKey key;
        
        do
        {
            if (clearScreen)
            {
                Console.Clear();
            }

            if (allowBack)
            {
                ConsoleHelper.PrintBackHint();
            }
            
            if (!string.IsNullOrWhiteSpace(title))
            {
                ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
                ConsoleHelper.PrintColorizeMessage(title, ConsoleColor.White);
                ConsoleHelper.PrintColorizeMessage("====================", ConsoleColor.DarkCyan);
            }
            
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    if (options[i] is "Exit" or "Logout" or "Delete Account")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (options[i] == "Back")
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                    Console.WriteLine($"> {options[i]}");
                    
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = options[i] == "Back"
                        ? ConsoleColor.Gray
                        : options[i] is "Exit" or "Logout" or "Delete Account"
                            ? ConsoleColor.Red
                            : ConsoleColor.White;
                    Console.WriteLine($"  {options[i]}");
                    Console.ResetColor();
                }
            }
            
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            key = keyInfo.Key;
            
            // B + Enter = Back
            if (allowBack &&
                (keyInfo.KeyChar == 'b' ||
                 keyInfo.KeyChar == 'B'))
            {
                ConsoleKeyInfo enter = Console.ReadKey(true);

                if (enter.Key == ConsoleKey.Enter)
                {
                    return -1;
                }

                continue;
            }

            if (key == ConsoleKey.DownArrow)
            {
                selectedIndex++;
                
                if (selectedIndex >= options.Length)
                {
                    selectedIndex = 0;
                }
            }
            
            else if (key == ConsoleKey.UpArrow)
            {
                selectedIndex--;


                if (selectedIndex < 0)
                {
                    selectedIndex = options.Length - 1;
                }
            }
            
        } while (key != ConsoleKey.Enter);
        
        return selectedIndex;
    }
}