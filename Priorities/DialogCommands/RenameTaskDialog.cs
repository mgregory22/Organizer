//
// Priorities/DialogCommands/RenameTaskDialog.cs
//

using System;
using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.TaskCommands;
using Priorities.Types;

namespace Priorities.DialogCommands
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
            IntEditor intEditor = new IntEditor(print, read);
            int? srcPriority;
            string name;

            srcPriority = intEditor.RangePrompt(1, tasks.Count, "Enter priority of item to rename\n# ");
            if (srcPriority == null)
            {
                return null;
            }
            name = editor.StringPrompt("Enter new name $ ");
            if (name == null)
            {
                return null;
            }
            return new RenameTask(tasks, srcPriority.Value - 1, name);
        }

    }
}
