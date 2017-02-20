//
// Priorities/Modules/Tasks/DialogCommands/AddTaskDialog.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Commands;

namespace Priorities.Modules.Tasks.DialogCommands
{
    /// <summary>
    /// AddDialog executes the Add Task dialog.
    /// Then, if successful, executes the Add Task command.
    /// </summary>
    public class AddTaskDialog : DialogCommand
    {
        protected Tasks tasks;

        /// <summary>
        /// Initializes AddDialog.
        /// </summary>
        /// <param name="print"></param>
        /// <param name="read"></param>
        /// <param name="addTask"></param>
        public AddTaskDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        /// <summary>
        /// Creates an add task command to be executed.
        /// </summary>
        public override Command Create()
        {
            Editor nameEditor = new Editor(print, read);
            string name = nameEditor.StringPrompt(NamePrompt);
            if (string.IsNullOrEmpty(name)) {
                print.StringNL("Add cancelled");
                return null;
            }
            IntEditor priorityEditor = new IntEditor(print, read);
            int? priority = priorityEditor.IntPrompt(PriorityPrompt);
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
