//
// Priorities/Modules/Tasks/DialogCommands/UndoDialog.cs
//

using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Commands;

namespace Priorities.Modules.Tasks.DialogCommands
{
    public class UndoDialog : DialogCommand
    {
        protected Tasks tasks;

        public UndoDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            return new UndoTask(undoManager, tasks);
        }
    }
}
