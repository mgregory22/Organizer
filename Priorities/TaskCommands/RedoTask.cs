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

        public override void Do()
        {
            // Do the redo!
            undoManager.Redo();
        }

        public override void Undo()
        {
            // Undo the redo = Undo!
            undoManager.Undo();
        }
    }
}
