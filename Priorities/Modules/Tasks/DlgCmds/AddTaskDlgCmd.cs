//
// Priorities/Modules/Tasks/DlgCmds/AddTaskDlgCmd.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Cmds;

namespace Priorities.Modules.Tasks.DlgCmds
{
    /// <summary>
    /// AddDialog executes the Add Task dialog.
    /// Then, if successful, executes the Add Task command.
    /// </summary>
    public class AddTaskDlgCmd : DlgUnCmd
    {
        protected Tasks tasks;

        /// <summary>
        /// Initializes AddDialog.
        /// </summary>
        /// <param name="io"></param>
        /// <param name="addTask"></param>
        public AddTaskDlgCmd(Io io, UndoManager undoManager, Tasks tasks)
            : base(io, undoManager)
        {
            this.tasks = tasks;
        }

        /// <summary>
        /// Creates an add task command to be executed.
        /// </summary>
        public override UnCmd Create()
        {
            Editor nameEditor = new Editor();
            string name = nameEditor.StringPrompt(io, NamePrompt);
            if (string.IsNullOrEmpty(name)) {
                io.print.StringNL("Add cancelled");
                return null;
            }
            IntEditor priorityEditor = new IntEditor();
            int? priority = priorityEditor.IntPrompt(io, PriorityPrompt);
            if (priority == null) {
                return new AddTask(tasks, name);
            }
            int index = priority.Value - 1;
            return new InsertTask(tasks, name, index);
        }

        public string PriorityPrompt {
            get { return "\nEnter priority (Enter blank to add to the end)\n# "; }
        }

        public string NamePrompt {
            get { return "\nEnter task name/description\n$ "; }
        }
    }
}
