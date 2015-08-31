//
// Priorities/Commands/RenameTask.cs
//

using MSG.IO;

namespace Priorities.Commands
{
    public class RenameTask : TaskCommand
    {
        public RenameTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {
        }
    }
}
