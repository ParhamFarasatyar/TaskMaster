namespace Menu;

public class ConsoleMenu
{
    public static int Show(string title, string[] options)
    {
        int selectedIndex = 0;

        ConsoleKey key;


        do
        {
            Console.Clear();


            Console.WriteLine("====================");
            Console.WriteLine(title);
            Console.WriteLine("====================");


            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }


            key = Console.ReadKey(true).Key;


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