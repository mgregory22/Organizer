//
// Priorities/Features/Tasks/Commands/UndoTask.cs
//

using MSG.Patterns;

namespace Priorities.Features.Tasks.Commands
{
    public class UndoTask : TaskCommand
    {
        protected UndoManager undoManager;

        public UndoTask(UndoManager undoManager, Tasks tasks)
            : base(tasks)
        {
            this.undoManager = undoManager;
        }

        public override Result Do()
        {
            // Do the undo!
            return undoManager.Undo();
        }

        public override Result Undo()
        {
            // Undo the undo = Redo!
            return undoManager.Redo();
        }
    }
}
