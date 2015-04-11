using MSG.IO;
using MSG.Patterns;

namespace Priorities.Commands
{
    class Quit : TaskCommand
    {
        public Quit(Print print, Read read, Tasks tasks)
            : base(print, read, tasks)
        {
        }
        public override int Do()
        {
            return 1;
        }
    }
}
