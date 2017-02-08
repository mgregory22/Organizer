//
// Priorities/TaskCommands/RedoTask.cs
//

using System;
using MSG.Patterns;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    public class RedoTask : TaskCommand
    {
        protected UndoManager undoManager;

        public RedoTask(UndoManager undoManager, Tasks tasks)
            : base(tasks)
        {
        }

        public override Result Do()
        {
            // Do the redo!
            return undoManager.Redo();
        }

        public override Result Undo()
        {
            // Undo the redo = Undo!
            return undoManager.Undo();
        }
    }
}
