//
// Priorities/Modules/Tasks/DialogCommands/DeleteTaskDialog.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Commands;

namespace Priorities.Modules.Tasks.DialogCommands
{
    public class DeleteTaskDialog : DialogCommand
    {
        protected Tasks tasks;

        public DeleteTaskDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            IntEditor editor = new IntEditor(print, read);
            int? priority = null;

            // If there's only one item, then just delete that item
            if (tasks.Count == 1)
                priority = 0;
            else
            {
                while (priority == null)
                {
                    // Ask user for priority of task to delete
                    priority = editor.RangePrompt(1, tasks.Count, Prompt);
                    // If escape was pressed, abort
                    if (priority == null)
                    {
                        return null;
                    }
                }
            }
            return new DeleteTask(tasks, priority.Value - 1);
        }

        public string Prompt {
            get { return "\nEnter priority of item to delete\n# "; }
        }
    }
}
