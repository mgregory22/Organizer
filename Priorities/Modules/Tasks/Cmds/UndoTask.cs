//
// Priorities/Modules/Tasks/Commands/UndoTask.cs
//

using MSG.Patterns;

namespace Priorities.Modules.Tasks.Cmds
{
    public class UndoTask : TaskCmd
    {
        protected UndoAndRedo undoAndRedo;

        public UndoTask(UndoAndRedo undoAndRedo, Tasks tasks)
            : base(tasks)
        {
            this.undoAndRedo = undoAndRedo;
        }

        public override Result Do()
        {
            // Do the undo!
            return undoAndRedo.Undo();
        }

        public override Result Undo()
        {
            // Undo the undo = Redo!
            return undoAndRedo.Redo();
        }
    }
}
