using MSG.IO;
using MSG.Patterns;

namespace Priorities.Commands
{
    class Quit : Command
    {
        public override int Do(Print print, Read read)
        {
            return 1;
        }
    }
}
