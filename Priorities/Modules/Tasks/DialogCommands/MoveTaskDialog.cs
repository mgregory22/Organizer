//
// Priorities/Modules/Tasks/DialogCommands/MoveTaskDialog.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Commands;

namespace Priorities.Modules.Tasks.DialogCommands
{
    public class MoveTaskDialog : DialogCommand
    {
        Tasks tasks;

        public MoveTaskDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            IntEditor intEditor = new IntEditor(print, read);
            int? srcPriority;
            int? destPriority;

            srcPriority = intEditor.RangePrompt(1, tasks.Count, PromptForSrc);
            if (srcPriority == null)
            {
                return null;
            }
            destPriority = intEditor.RangePrompt(1, tasks.Count, PromptForDest);
            if (destPriority == null)
            {
                return null;
            }
            return new MoveTask(tasks, srcPriority.Value - 1, destPriority.Value - 1);
        }

        public string PromptForDest {
            get { return "Enter new priority\n# "; }
        }

        public string PromptForSrc {
            get { return "\nEnter priority of item to change\n# "; }
        }
    }
}
;
