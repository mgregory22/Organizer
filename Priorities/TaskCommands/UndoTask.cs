//
// Priorities/TaskCommands/UndoTask.cs
//

using System;
using MSG.Patterns;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    public class UndoTask : TaskCommand
    {
        protected UndoManager undoManager;

        public UndoTask(UndoManager undoManager, Tasks tasks)
            : base(tasks)
        {
            this.undoManager = undoManager;
        }

        public override void Do()
        {
            // Do the undo!
            undoManager.Undo();
        }

        public override void Undo()
        {
            // Undo the undo = Redo!
            undoManager.Redo();
        }
    }
}
