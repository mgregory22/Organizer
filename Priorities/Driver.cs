//
// Priorities/Driver.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using MSG.Patterns.Conds;
using Priorities.DlgCmds;
using Priorities.Modules.Tasks;
using Priorities.Modules.Tasks.Conds;
using Priorities.Modules.Tasks.DlgCmds;

namespace Priorities
{
    /// <summary>
    /// Displays menu and performs commands
    /// </summary>
    public class Driver : Cmd
    {
        /// <summary>
        /// Sets up and starts the main loop of the program.
        /// </summary>
        /// <param name="prompt">
        /// 
        /// </param>
        public static void Do(Io io, CharPrompt prompt)
        {
            // Program data
            Tasks tasks = new Tasks();
            UndoAndRedo undoAndRedo = new UndoAndRedo();

            Menu taskMenu = BuildTaskMenu(io, prompt, tasks, undoAndRedo);
            Menu mainMenu = BuildMainMenu(io, prompt, taskMenu);
            mainMenu.Loop(io);
        }

        /// <summary>
        /// Builds main menu
        /// </summary>
        public static Menu BuildMainMenu(Io io, CharPrompt prompt, Menu taskMenu)
        {
            Menu menu = new Menu(io, "Main Menu", prompt);
            MenuItem[] mainMenuItems = new MenuItem[] {
                new MenuItem('n', "New File",
                    Cond.ALWAYS,
                    new NewFileDlgCmd(io)),
                new MenuItem('o', "Open File",
                    Cond.ALWAYS,
                    new OpenFileDlgCmd(io)),
                new MenuItem('t', "Tasks",
                    Cond.ALWAYS,
                    taskMenu),
                new MenuItem('q', "Quit Program",
                    Cond.ALWAYS,
                    new QuitProgDlgCmd(io)),
            };
            menu.AddMenuItems(mainMenuItems);
            return menu;
        }

        /// <summary>
        /// Builds task menu
        /// </summary>
        public static Menu BuildTaskMenu(Io io, CharPrompt prompt, Tasks tasks, UndoAndRedo undoAndRedo)
        {
            // Conditions related to task data for enabling and disabling menu items
            IsTasksNonEmpty isTasksNonEmpty = new IsTasksNonEmpty(tasks);
            IsTasksMoreThanOne isTasksMoreThanOne = new IsTasksMoreThanOne(tasks);
            IsUndoStackEmpty canUndo = new IsUndoStackEmpty(undoAndRedo);
            IsRedoStackEmpty canRedo = new IsRedoStackEmpty(undoAndRedo);
            
            // Build task menu
            Menu taskMenu = new Menu(io, "Task Menu", prompt);
            MenuItem[] taskMenuItems = new MenuItem[] {
                new MenuItem('a', "Add Task",
                    Cond.ALWAYS,
                    new AddTaskDlgCmd(io, undoAndRedo, tasks)
                ),
                new MenuItem('d', "Delete Task",
                    isTasksNonEmpty,
                    new DeleteTaskDlgCmd(io, undoAndRedo, tasks)
                ),
                new MenuItem('l', "List Tasks",
                    Cond.ALWAYS,
                    new ListTasksDlgCmd(io, tasks)
                ),
                new MenuItem('m', "Move Task/Change Priority",
                    isTasksMoreThanOne,
                    new MoveTaskDlgCmd(io, undoAndRedo, tasks)
                ),
                new MenuItem('q', "Quit Program",
                    Cond.ALWAYS,
                    new QuitProgDlgCmd(io)
                ),
                new MenuItem('r', "Rename Task",
                    isTasksNonEmpty,
                    new RenameTaskDlgCmd(io, undoAndRedo, tasks)
                ),
                new MenuItem('s', "Edit Subtasks",
                    isTasksNonEmpty,
                    new EditSubtasksDlgCmd(io, undoAndRedo, tasks)
                ),
                new MenuItem('u', "Up To Main",
                    Cond.ALWAYS,
                    new UpMenuDlgCmd(io)
                ),
                new MenuItem('z', "Undo Last Action",
                    canUndo,
                    new UndoDlgCmd(io, undoAndRedo, tasks)
                ),
                new MenuItem('Z', "Redo Next Action",
                    canRedo,
                    new RedoDlgCmd(io, undoAndRedo, tasks)
                ),
            };
            taskMenu.AddMenuItems(taskMenuItems);
            return taskMenu;
        }
    }
}
