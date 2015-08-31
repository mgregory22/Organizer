﻿//
// Priorities/Commands/Help.cs
//

using MSG.IO;
using MSG.Patterns;

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

        override public void Do()
        {
            print.String(target.ToString());
        }
    }
}
