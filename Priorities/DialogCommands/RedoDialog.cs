//
// Priorities/DialogCommands/RedoTaskDialog.cs
//

using System;
using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.TaskCommands;
using Priorities.Types;

namespace Priorities.DialogCommands
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


        public override bool IsEnabled()
        {
            return undoManager.CanRedo();
        }

    }
}
