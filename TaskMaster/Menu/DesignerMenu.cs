using UserModel;
namespace Menu;

public class DesignerMenu
{
    public void Show(User user)
    {
        Console.WriteLine("======================");
        Console.WriteLine("    DESIGNER MENU     ");
        Console.WriteLine("======================");


        Console.WriteLine("1. Add Question");
        Console.WriteLine("2. Edit Question");
        Console.WriteLine("3. Remove Question");
        Console.WriteLine("4. Review Answers");
        Console.WriteLine("5. Logout");
        
        Console.Write("Choose option: ");

        string? choice = Console.ReadLine();
        
        switch(choice)
        {
            case "1":
                // Add question logic belongs to Designer module
                Console.WriteLine("Add Question Selected");
                break;
            
            case "2":
                // Edit question logic belongs to Designer module
                Console.WriteLine("Edit Question Selected");
                break;
            
            case "3":
                // Remove question logic belongs to Designer module
                Console.WriteLine("Remove Question Selected");
                break;
            
            case "4":
                // Review logic belongs to Answer module
                Console.WriteLine("Review Answers Selected");
                break;
            
            case "5":
                Console.WriteLine("Logout Selected");
                break;
            
            default:
                // Validation will be handled elsewhere
                Console.WriteLine("Invalid option");
                break;
        }
    }
}