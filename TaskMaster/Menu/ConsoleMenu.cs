namespace Menu;
public class ConsoleMenu
{
    public static int Show(
        string title,
        string[] options,
        bool allowBack = true,
        bool clearScreen = true)
    {
        int selectedIndex = 0;

        ConsoleKey key;
        
        do
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            
            if (!string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("====================");
                Console.WriteLine(title);
                Console.WriteLine("====================");
            }
            
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    if (options[i] == "Logout" ||
                        options[i] == "Delete Account")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    
                    Console.WriteLine($"> {options[i]}");
                    
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
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