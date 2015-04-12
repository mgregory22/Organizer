using MSG.IO;
using MSG.Patterns;

namespace Priorities.Commands
{
    public class Quit : TaskCommand
    {
        public static int quitValue = 65535;
        public Quit(Print print, Read read, Tasks tasks)
            : base(print, read, tasks)
        {
        }
        public override int Do()
        {
            return quitValue;
        }
    }
}
