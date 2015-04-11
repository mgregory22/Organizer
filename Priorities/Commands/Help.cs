using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class Help : TaskCommand
    {
        public Help(Print print, Read read, Tasks tasks)
            : base(print, read, tasks)
        {
        }
    }
}
