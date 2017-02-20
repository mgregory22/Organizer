//
// Priorities/Modules/Tasks/DialogCommands/RedoDialog.cs
//

using MSG.IO;
using MSG.Patterns;
using Priorities.Modules.Tasks.Commands;

namespace Priorities.Modules.Tasks.DialogCommands
{
    public class RedoDialog : DialogCommand
    {
        protected Tasks tasks;

        public RedoDialog(Print print, Read read, UndoManager undoManager, Tasks tasks)
            : base(print, read, undoManager)
        {
            this.tasks = tasks;
        }

        public override Command Create()
        {
            return new RedoTask(undoManager, tasks);
        }
    }
}
