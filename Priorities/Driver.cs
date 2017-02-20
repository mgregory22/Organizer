//
// Priorities/Driver.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using MSG.Patterns.Conditions;
using Priorities.DialogCommands;
using Priorities.Modules.Tasks;
using Priorities.Modules.Tasks.Conditions;
using Priorities.Modules.Tasks.DialogCommands;

namespace Priorities
{
    /// <summary>
    /// This is the heart of the program.  Testing starts with this class.
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Sets up and starts the main loop of the program.
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

            // Conditions related to task data for enabling and disabling menu items
            IsTasksNonEmpty isTasksNonEmpty = new IsTasksNonEmpty(tasks);
            IsTasksMoreThanOne isTasksMoreThanOne = new IsTasksMoreThanOne(tasks);
            CanUndo canUndo = new CanUndo(undoManager);
            CanRedo canRedo = new CanRedo(undoManager);

            // Task Menu
            Menu taskMenu = new Menu("Task Menu", prompt);
            MenuItem[] taskMenuItems = new MenuItem[] {
                    new MenuItem('a', "Add Task", new AddTaskDialog(print, read, undoManager, tasks), Condition.always),
                    new MenuItem('d', "Delete Task", new DeleteTaskDialog(print, read, undoManager, tasks), isTasksNonEmpty),
                    new MenuItem('l', "List Tasks", new ListTasksDialog(print, tasks), Condition.always),
                    new MenuItem('m', "Move Task/Change Priority", new MoveTaskDialog(print, read, undoManager, tasks), isTasksMoreThanOne),
                    new MenuItem('q', "Quit Program", new QuitProgramDialog(print, read), Condition.always),
                    new MenuItem('r', "Rename Task", new RenameTaskDialog(print, read, undoManager, tasks), isTasksNonEmpty),
                    new MenuItem('s', "Edit Subtasks", new EditSubtasksDialog(print, read, undoManager, tasks), isTasksNonEmpty),
                    new MenuItem('u', "Up To Main", new UpMenuDialog(print, read), Condition.always),
                    new MenuItem('z', "Undo Last Action", new UndoDialog(print, read, undoManager, tasks), canUndo),
                    new MenuItem('Z', "Redo Next Action", new RedoDialog(print, read, undoManager, tasks), canRedo)
                };
            taskMenu.AddMenuItems(taskMenuItems);

            // Main Menu
            Menu mainMenu = new Menu("Main Menu", prompt);
            MenuItem[] mainMenuItems = new MenuItem[] {
                    new MenuItem('a', "Admin Tools", new AdminToolsDialog(print, read), Condition.always),
                    new MenuItem('t', "Tasks", taskMenu, Condition.always),
                    new MenuItem('q', "Quit Program", new QuitProgramDialog(print, read), Condition.always),
                };
            mainMenu.AddMenuItems(mainMenuItems);
            mainMenu.Loop();
        }
    }
}
