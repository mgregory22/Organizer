//
// Priorities/Commands/MoveTask.cs
//

using MSG.IO;

namespace Priorities.Commands
{
    public class MoveTask : TaskCommand
    {
        public MoveTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {
        }
    }
}
