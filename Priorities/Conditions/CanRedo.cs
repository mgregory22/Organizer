using MSG.Patterns;

namespace Priorities.Conditions
{
    public class CanRedo : Condition
    {
        protected UndoManager undoManager;

        public CanRedo(UndoManager undoManager)
        {
            this.undoManager = undoManager;
        }

        public override bool Test()
        {
            return undoManager.CanRedo();
        }
    }
}
