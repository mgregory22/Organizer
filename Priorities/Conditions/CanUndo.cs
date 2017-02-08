using MSG.Patterns;

namespace Priorities.Conditions
{
    public class CanUndo : Condition
    {
        protected UndoManager undoManager;

        public CanUndo(UndoManager undoManager)
        {
            this.undoManager = undoManager;
        }

        public override bool Test()
        {
            return undoManager.CanUndo();
        }
    }
}
