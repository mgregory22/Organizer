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
                new MenuItem('a', new AddTask(), "Add Task"),
                new MenuItem('d', new DeleteTask(), "Delete Task"),
                new MenuItem('l', new ListTasks(), "List Tasks"),
                new MenuItem('m', new MoveTask(), "Move Task/Change Priority"),
                new MenuItem('o', new Options(), "Options Menu"),
                new MenuItem('q', new Quit(), "Quit Program"),
                new MenuItem('r', new RenameTask(), "Rename Task")
            };
            Menu mainMenu = new Menu("Main Menu", menuItems);
            Console.WriteLine(mainMenu.ToString());
            Print print = new Print();
            Read read = new Read();
            KeyPrompt menuPrompt = new KeyPrompt(print, "> ", read);
            char key = menuPrompt.DoPrompt();
            System.Console.WriteLine();
            for (char c = ' '; c <= '~'; c++)
            {
                Console.Write(c);
            }
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
