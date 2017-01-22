//
// Priorities/TaskCommands/RenameTask.cs
//

using MSG.IO;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    public class RenameTask : TaskCommand
    {
        public RenameTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {
        }
    }
}
