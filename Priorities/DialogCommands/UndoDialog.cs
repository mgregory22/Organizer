//
// Priorities/DialogCommands/UndoDialog.cs
//

using System;
using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using Priorities.TaskCommands;
using Priorities.Types;

namespace Priorities.DialogCommands
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

        public override bool IsEnabled()
        {
            return undoManager.CanUndo();
        }
    }
}
