//
// Priorities/Driver.cs
//

using System.Collections.Generic;
using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.DialogCommands;
using Priorities.Types;
using Priorities.Conditions;

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
            // IO
            Print print = prompt.Print;
            Read read = prompt.Read;

            // Program data
            Tasks tasks = new Tasks();
            UndoManager undoManager = new UndoManager();

            // Conditions related to program data for enabling and disabling menu items
            Always always = new Always();
            IsTasksNonEmpty isTasksNonEmpty = new IsTasksNonEmpty(tasks);
            IsTasksMoreThanOne isTasksMoreThanOne = new IsTasksMoreThanOne(tasks);
            CanUndo canUndo = new CanUndo(undoManager);
            CanRedo canRedo = new CanRedo(undoManager);

            // Menu
            MenuItem[] menuItems = {
                    new MenuItem('a', "Add Task", new AddTaskDialog(print, read, undoManager, tasks), always),
                    new MenuItem('d', "Delete Task", new DeleteTaskDialog(print, read, undoManager, tasks), isTasksNonEmpty),
                    new MenuItem('l', "List Tasks", new ListTasksDialog(print, tasks), always),
                    new MenuItem('m', "Move Task/Change Priority", new MoveTaskDialog(print, read, undoManager, tasks), isTasksMoreThanOne),
                    new MenuItem('o', "Options Menu", new OptionsDialog(print, read), always),
                    new MenuItem('q', "Quit Program", new QuitDialog(print, read), always),
                    new MenuItem('r', "Rename Task", new RenameTaskDialog(print, read, undoManager, tasks), isTasksNonEmpty),
                    new MenuItem('z', "Undo Last Action", new UndoDialog(print, read, undoManager, tasks), canUndo),
                    new MenuItem('Z', "Redo Next Action", new RedoDialog(print, read, undoManager, tasks), canRedo)
                };
            Menu mainMenu = new Menu("Main Menu", menuItems, prompt);

            // The help item is a special case because it depends on the menu itself
            HelpDialog help = new HelpDialog(print, mainMenu);
            mainMenu.AddMenuItem(new MenuItem('?', "Help", help, always));
            mainMenu.Loop();
        }
    }
}
