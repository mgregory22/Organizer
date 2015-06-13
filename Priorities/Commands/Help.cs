using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    /// <summary>
    ///   The Help object just prints the ToString() output of
    ///   whatever object you send it.
    /// </summary>
    public class Help : Command
    {
        Print print;
        object target;

        public Help(Print print)
        {
            this.print = print;
        }

        public void SetTarget(object target)
        {
            this.target = target;
        }

        public override void Do()
        {
            print.String(target.ToString());
        }
    }
}
