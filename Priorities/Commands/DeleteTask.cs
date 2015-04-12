using MSG.IO;
using MSG.Patterns;
using System;

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
