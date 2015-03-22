using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class Options : Command
    {
        public override void Do()
        {
            Console.WriteLine("Go to options menu");
        }
        public override void Undo()
        {
            Console.WriteLine("Can't undo go to options menu!");
        }
    }
}
