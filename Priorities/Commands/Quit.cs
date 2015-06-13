using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    public class Quit : Command
    {
        public static int quitValue = 65535;

        public Quit()
        {
        }

        public override void Do()
        {
            throw new OperationCanceledException("User has quit");
        }
    }
}
