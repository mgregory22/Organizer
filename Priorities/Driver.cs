//
// Priorities/Driver.cs
//

using MSG.Console;
using MSG.IO;
using Priorities.Commands;

namespace Priorities
{
    /// <summary>
    ///   This is the heart of the program.  Testing starts with this class.
    /// </summary>
    public class Driver
    {
        public static void Run(Print print, Read read, CharPrompt prompt)
        {
            Tasks tasks = new Tasks();
            // Help is weird because of the circular dependency on the menu.
            Help help = new Help(print);
            MenuItem[] menuItems = {
                    new MenuItem('a', new AddTask(print, read, tasks), "Add Task"),
                    new MenuItem('d', new DeleteTask(print, read, tasks), "Delete Task"),
                    new MenuItem('l', new ListTasks(print, tasks), "List Tasks"),
                    new MenuItem('m', new MoveTask(print, read, tasks), "Move Task/Change Priority"),
                    new MenuItem('o', new Options(print, read), "Options Menu"),
                    new MenuItem('q', new Quit(), "Quit Program"),
                    new MenuItem('r', new RenameTask(print, read, tasks), "Rename Task"),
                    new MenuItem('?', help, "Help")
                };
            Menu mainMenu = new Menu("Main Menu", menuItems, prompt);
            help.SetTarget(mainMenu);
            mainMenu.Loop();
        }
    }
}
