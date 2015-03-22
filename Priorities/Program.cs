using MSG.Console;
using Priorities.Commands;
using System;

namespace Priorities
{
    public sealed class Program
    {
        public static string promptMsg = "] ";

        public static void Main(string[] args)
        {
            Quit quitCmd = new Quit();
            MenuItem[] menuItems = {
                new MenuItem('a', new AddTask(), "Add Task"),
                new MenuItem('d', new DeleteTask(), "Delete Task"),
                new MenuItem('l', new ListTasks(), "List Tasks"),
                new MenuItem('m', new MoveTask(), "Move Task/Change Priority"),
                new MenuItem('o', new Options(), "Options Menu"),
                new MenuItem('q', quitCmd, "Quit Program"),
                new MenuItem('r', new RenameTask(), "Rename Task")
            };
            Menu mainMenu = new Menu("Main Menu", menuItems);
            Print print = new Print();
            Read read = new Read();
            KeyPrompt menuPrompt = new KeyPrompt(print, promptMsg, read);
            menuPrompt.ValidList = new char[] { 'a', 'd', 'l', 'm', 'o', 'q', 'r' };
            // Main loop
            do {
                print.String(mainMenu.ToString());
                char key = menuPrompt.DoPrompt();
                mainMenu.DoMatchingItem(key);
                print.Newline(); // intercommand newline
            } while (!quitCmd.Done);
            // Pause before exiting
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
