//
// Priorities/Commands/DeleteTask.cs
//

using MSG.IO;

namespace Priorities.Commands
{
    class DeleteTask : TaskCommand
    {
        public DeleteTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {

        }
    }
}
