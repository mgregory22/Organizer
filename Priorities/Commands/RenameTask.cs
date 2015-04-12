using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    public class RenameTask : TaskCommand
    {
        public RenameTask(Print print, Read read, Tasks tasks)
            : base(print, read, tasks)
        {
        }
    }
}
