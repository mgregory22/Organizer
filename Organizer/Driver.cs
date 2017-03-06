//
// Priorities/Driver.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using MSG.Patterns.Conds;
using MSG.Types.Dir;
using Organizer.Modules.Base.DlgCmds;
using Organizer.Modules.File.DlgCmds;
using Organizer.Modules.Tasks;
using Organizer.Modules.Tasks.Conds;
using Organizer.Modules.Tasks.DlgCmds;

namespace Organizer
{
    /// <summary>
    /// Displays menu and performs commands
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Sets up and starts the main loop of the program.
        /// </summary>
        public static void Run(Io io, CharPrompt prompt, Dir<Task> tasksDir)
        {
            Tasks tasks = new Tasks(tasksDir);
            TaskMenuPrompt taskMenuPrompt = new TaskMenuPrompt(tasks);
            UndoAndRedo undoAndRedo = new UndoAndRedo();

            Menu taskMenu = BuildTaskMenu(io, taskMenuPrompt, tasks, undoAndRedo);
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
        public static Menu BuildTaskMenu(Io io, TaskMenuPrompt prompt, Tasks tasks, UndoAndRedo undoAndRedo)
        {
            // Conditions related to task data for enabling and disabling menu items
            IsTasksNonEmpty isTasksNonEmpty = new IsTasksNonEmpty(tasks);
            IsTasksMoreThanOne isTasksMoreThanOne = new IsTasksMoreThanOne(tasks);
            IsCurTaskDirNotRoot isCurTaskDirNotRoot = new IsCurTaskDirNotRoot(tasks);
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
                new MenuItem('q', "Quit To Main",
                    Cond.ALWAYS,
                    new UpMenuDlgCmd(io)
                ),
                new MenuItem('r', "Rename Task",
                    isTasksNonEmpty,
                    new RenameTaskDlgCmd(io, undoAndRedo, tasks)
                ),
                new MenuItem('s', "Edit Subtasks",
                    isTasksNonEmpty,
                    new EditSubtasksDlgCmd(io, tasks)
                ),
                new MenuItem('u', "Up To Parent Task",
                    isCurTaskDirNotRoot,
                    new UpToParentTaskDlgCmd(io, tasks)
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
