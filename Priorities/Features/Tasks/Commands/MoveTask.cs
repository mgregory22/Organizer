//
// Priorities/Features/Tasks/Commands/MoveTask.cs
//

namespace Priorities.Features.Tasks.Commands
{
    public class MoveTask : TaskCommand
    {
        protected int srcPosition;
        protected int destPosition;

        public MoveTask(Tasks tasks, int srcPosition, int destPosition)
            : base(tasks)
        {
            this.srcPosition = srcPosition;
            this.destPosition = destPosition;
        }

        public override Result Do()
        {
            tasks.Move(srcPosition, destPosition);
            return new Ok();
        }

        public override Result Undo()
        {
            tasks.Move(destPosition, srcPosition);
            return new Ok();
        }
    }
}
