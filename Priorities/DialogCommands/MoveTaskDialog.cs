//
// Priorities/DialogCommands/MoveTaskDialog.cs
//

using System;
using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.TaskCommands;
using Priorities.Types;

namespace Priorities.DialogCommands
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

            srcPriority = intEditor.RangePrompt(1, tasks.Count, "Enter priority of item to change\n# ");
            if (srcPriority == null)
            {
                return null;
            }
            destPriority = intEditor.RangePrompt(1, tasks.Count, "Enter new priority # ");
            if (destPriority == null)
            {
                return null;
            }
            return new MoveTask(tasks, srcPriority.Value - 1, destPriority.Value - 1);
        }
    }
}
;