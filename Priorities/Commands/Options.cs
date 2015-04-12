using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    public class Options : TaskCommand
    {
        public Options(Print print, Read read, Tasks tasks)
            : base(print, read, tasks)
        {
        }
    }
}
