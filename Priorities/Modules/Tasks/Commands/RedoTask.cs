//
// Priorities/Modules/Tasks/Commands/RedoTask.cs
//

using MSG.Patterns;

namespace Priorities.Modules.Tasks.Commands
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
