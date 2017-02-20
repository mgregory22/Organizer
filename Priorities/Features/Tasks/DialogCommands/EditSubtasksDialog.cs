//
// Priorities/Features/Tasks/DialogCommands/CreateSubtaskDialog.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Features.Tasks.Commands;

namespace Priorities.Features.Tasks.DialogCommands
{
    public class EditSubtasksDialog : DialogCommand
    {
        protected Tasks tasks;

        public EditSubtasksDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            Editor editor = new Editor(print, read);
            IntEditor intEditor = new IntEditor(print, read);
            int? targetPriority;
            string name;

            targetPriority = intEditor.RangePrompt(1, tasks.Count, PromptForTarget);
            if (targetPriority == null)
            {
                return null;
            }
            int index = targetPriority.Value - 1;
            name = editor.StringPrompt(PromptForName);
            if (name == null)
            {
                return null;
            }
            return new EditSubtasks(tasks, index, name);
        }

        public string PromptForName {
            get { return "Enter new task name/description\n$ "; }
        }

        public string PromptForTarget {
            get { return "\nEnter priority of item to create subtask for\n# "; }
        }
    }
}
