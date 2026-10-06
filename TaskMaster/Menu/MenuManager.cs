using UserModel;
namespace Menu;

public class MenuManager
{
    public bool Show(User user)
    {
        if(user.Role == Role.Intern)
        {
            InternMenu internMenu = new InternMenu();
            return internMenu.Show(user);
        }
        else if(user.Role == Role.Designer)
        {
            DesignerMenu designerMenu = new DesignerMenu();
            return designerMenu.Show(user);
        }

        return true;
    }
}