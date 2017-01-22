//
// Priorities/TaskCommands/MoveTask.cs
//

using MSG.IO;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    public class MoveTask : TaskCommand
    {
        public MoveTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {
        }
    }
}
