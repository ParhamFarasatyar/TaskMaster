// using UserModel;
//
// namespace Menu;
//
// public class InternProfile
// {
//     public void Show(User user)
//     {
//         while (true)
//         {
//             string[] options =
//             {
//                 "Edit Profile",
//                 "Delete Account",
//                 "Back"
//             };
//
//
//             int selected = 0;
//
//             ConsoleKey key;
//
//
//             do
//             {
//                 Console.Clear();
//
//
//                 Console.WriteLine("====================");
//                 Console.WriteLine("    INTERN INFO     ");
//                 Console.WriteLine("====================");
//
//
//                 Console.WriteLine(
//                     $"Full Name : {user.Name} {user.LastName}"
//                 );
//
//
//                 Console.WriteLine(
//                     $"Username  : {user.UserName}"
//                 );
//
//
//                 Console.WriteLine(
//                     $"Level     : {user.Level}"
//                 );
//
//
//                 Console.WriteLine(
//                     $"Score     : {user.Score}"
//                 );
//
//
//                 Console.WriteLine();
//
//
//
//                 for (int i = 0; i < options.Length; i++)
//                 {
//                     if (i == selected)
//                     {
//                         if (options[i] == "Delete Account")
//                         {
//                             Console.ForegroundColor = ConsoleColor.Red;
//                         }
//                         else
//                         {
//                             Console.ForegroundColor = ConsoleColor.Cyan;
//                         }
//
//
//                         Console.WriteLine($"> {options[i]}");
//
//
//                         Console.ResetColor();
//                     }
//                     else
//                     {
//                         Console.WriteLine($"  {options[i]}");
//                     }
//                 }
//
//
//
//                 ConsoleKeyInfo keyInfo = Console.ReadKey(true);
//
//                 key = keyInfo.Key;
//
//
//
//                 if (key == ConsoleKey.F2)
//                 {
//                     return;
//                 }
//
//
//
//                 if (key == ConsoleKey.DownArrow)
//                 {
//                     selected++;
//
//                     if (selected >= options.Length)
//                     {
//                         selected = 0;
//                     }
//                 }
//
//
//
//                 else if (key == ConsoleKey.UpArrow)
//                 {
//                     selected--;
//
//                     if (selected < 0)
//                     {
//                         selected = options.Length - 1;
//                     }
//                 }
//
//
//
//             } while (key != ConsoleKey.Enter);
//
//
//
//             switch (selected)
//             {
//                 case 0:
//
//                     EditProfile(user);
//
//                     break;
//
//
//
//                 case 1:
//
//                     if (DeleteAccount(user))
//                     {
//                         return;
//                     }
//
//                     break;
//
//
//
//                 case 2:
//
//                     return;
//             }
//         }
//     }
//
//
//
//
//
//     private void EditProfile(User user)
//     {
//         while (true)
//         {
//             string[] fields =
//             {
//                 "Name",
//                 "LastName",
//                 "UserName",
//                 "Password",
//                 "Back"
//             };
//
//
//             int selected = ConsoleMenu.Show(
//                 "Edit Profile",
//                 fields
//             );
//
//
//             if (selected == -1 || selected == 4)
//             {
//                 return;
//             }
//
//
//
//             string field = fields[selected];
//
//
//
//             string? value = ConsoleHelper.ReadInput(
//                 $"New {field} (F2 = Back): "
//             );
//
//
//             if (value == null)
//             {
//                 continue;
//             }
//
//
//
//             bool result = user.Edit(
//                 field,
//                 value
//             );
//
//
//
//             if (result)
//             {
//                 ConsoleHelper.PrintColorizeMessage(
//                     "Profile Updated Successfully!",
//                     ConsoleColor.Green
//                 );
//             }
//             else
//             {
//                 ConsoleHelper.PrintColorizeMessage(
//                     "Update Failed!",
//                     ConsoleColor.Red
//                 );
//             }
//
//
//             Console.ReadLine();
//         }
//     }
//
//
//
//
//
//     private bool DeleteAccount(User user)
//     {
//         string[] options =
//         {
//             "Yes, Delete",
//             "No, Back"
//         };
//
//
//         int selected = ConsoleMenu.Show(
//             "Delete Account?",
//             options
//         );
//
//
//         if (selected == -1 || selected == 1)
//         {
//             return false;
//         }
//
//
//
//         bool result = user.Remove();
//
//
//
//         if (result)
//         {
//             ConsoleHelper.PrintColorizeMessage(
//                 "Account Deleted Successfully!",
//                 ConsoleColor.Green
//             );
//
//
//             Console.ReadLine();
//
//
//             MainMenu mainMenu = new();
//
//             mainMenu.Show();
//
//
//             return true;
//         }
//         
//         return false;
//     }
// }