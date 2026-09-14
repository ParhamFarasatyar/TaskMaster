using UserModel;
namespace Menu;

public class MenuManager
{
    public void Show(User user)
    {
        if(user.Role == Role.Intern)
        {
            InternMenu internMenu = new InternMenu();
            internMenu.Show(user);
        }
        else if(user.Role == Role.Designer)
        {
            DesignerMenu designerMenu = new DesignerMenu();
            designerMenu.Show(user);
        }
    }
}