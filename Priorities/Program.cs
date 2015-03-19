using MSG.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            MenuItem[] menuItems = {
                new MenuItem(ConsoleKey.A, new AddTask(), "Add Task"),
                new MenuItem(ConsoleKey.D, new DeleteTask(), "Delete Task"),
                new MenuItem(ConsoleKey.L, new ListTasks(), "List Tasks"),
                new MenuItem(ConsoleKey.M, new MoveTask(), "Move Task/Change Priority"),
                new MenuItem(ConsoleKey.O, new Options(), "Options Menu"),
                new MenuItem(ConsoleKey.Q, new Quit(), "Quit Program"),
                new MenuItem(ConsoleKey.R, new RenameTask(), "Rename Task")
            };
            Menu mainMenu = new Menu("Main Menu", menuItems);
            Console.WriteLine(mainMenu.ToString());
            Prompt menuPrompt = new Prompt("> ");
            ConsoleKey key = menuPrompt.ReadKey();
            System.Console.WriteLine();
            Pause();
        }
        /// <summary>
        ///   Wait for key to keep the window open.
        /// </summary>
        public static void Pause()
        {
            Console.Write("Press any key to close window");
            Console.ReadKey(true);
        }
    }
}
