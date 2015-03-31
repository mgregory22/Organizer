using MSG.Patterns;

namespace Priorities.Commands
{
    class Quit : Command
    {
        public override int Do()
        {
            return 1;
        }
    }
}
