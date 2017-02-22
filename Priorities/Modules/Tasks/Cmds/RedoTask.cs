//
// Priorities/Modules/Tasks/Commands/RedoTask.cs
//

using MSG.Patterns;

namespace Priorities.Modules.Tasks.Cmds
{
    public class RedoTask : TaskCmd
    {
        protected UndoAndRedo undoAndRedo;

        public RedoTask(UndoAndRedo undoAndRedo, Tasks tasks)
            : base(tasks)
        {
        }

        public override Result Do()
        {
            // Do the redo!
            return undoAndRedo.Redo();
        }

        public override Result Undo()
        {
            // Undo the redo = Undo!
            return undoAndRedo.Undo();
        }
    }
}
