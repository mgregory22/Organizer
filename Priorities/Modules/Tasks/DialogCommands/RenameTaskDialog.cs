//
// Priorities/Modules/Tasks/DialogCommands/RenameTaskDialog.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Commands;

namespace Priorities.Modules.Tasks.DialogCommands
{
    public class RenameTaskDialog : DialogCommand
    {
        protected Tasks tasks;

        public RenameTaskDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            Editor editor = new Editor(print, read);
            IntEditor intEditor = new IntEditor(print, read);
            int? srcPriority;
            string name;

            srcPriority = intEditor.RangePrompt(1, tasks.Count, PromptForTarget);
            if (srcPriority == null)
            {
                return null;
            }
            name = editor.StringPrompt(PromptForName);
            if (name == null)
            {
                return null;
            }
            return new RenameTask(tasks, srcPriority.Value - 1, name);
        }

        public string PromptForName {
            get { return "Enter new name\n$ "; }
        }

        public string PromptForTarget {
            get { return "\nEnter priority of item to rename\n# "; }
        }
    }
}
