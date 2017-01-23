//
// Priorities/Driver.cs
//

using System.Collections.Generic;
using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.DialogCommands;
using Priorities.TaskCommands;
using Priorities.Types;

namespace Priorities
{
    /// <summary>
    ///   This is the heart of the program.  Testing starts with this class.
    /// </summary>
    public class Driver
    {
        /// <summary>
        ///   Sets up and starts the main loop of the program.
        /// </summary>
        /// <param name="prompt">
        ///   
        /// </param>
        public static void Run(CharPrompt prompt)
        {
            Print print = prompt.Print;
            Read read = prompt.Read;
            Tasks tasks = new Tasks();
            UndoManager undoManager = new UndoManager();
            MenuItem[] menuItems = {
                    new MenuItem('a', "Add Task", new AddTaskDialog(print, read, undoManager, tasks)),
                    new MenuItem('d', "Delete Task", new DeleteTaskDialog(print, read, undoManager, tasks)),
                    new MenuItem('l', "List Tasks", new ListTasksDialog(print, tasks)),
                    new MenuItem('m', "Move Task/Change Priority", new MoveTaskDialog(print, read, undoManager, tasks)),
                    new MenuItem('o', "Options Menu", new OptionsDialog(print, read)),
                    new MenuItem('q', "Quit Program", new QuitDialog(print, read)),
                    new MenuItem('r', "Rename Task", new RenameTaskDialog(print, read, undoManager, tasks)),
                    new MenuItem('z', "Undo Last Action", new UndoDialog(print, read, undoManager, tasks)),
                    new MenuItem('Z', "Redo Next Action", new RedoDialog(print, read, undoManager, tasks))
                };
            Menu mainMenu = new Menu("Main Menu", menuItems, prompt);

            // The help item is a special case
            HelpDialog help = new HelpDialog(print, mainMenu);
            mainMenu.AddMenuItem(new MenuItem('?', "Help", help));
            mainMenu.Loop();
        }
    }
}
