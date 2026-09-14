// namespace TaskMaster;
//
// class Program
// {
//     static void Main(string[] args)
//     {
//         Console.WriteLine("Hello, World!");
//     }
// }

using Menu;
using UserModel;


User user = new User(
    Role.Intern,
    "Aryan",
    "Test",
    "aryan123",
    "1234",
    Level.Beginner,
    0
);


InternMenu internMenu = new InternMenu();

internMenu.ShowMenu(user);