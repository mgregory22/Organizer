using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Patterns
{
    /// <summary>
    ///   Implements a basic Command design pattern.
    /// </summary>
    public interface Command
    {
        /// <summary>
        ///   Performs the command and somehow notes what was done so it can be possibly undone.
        /// </summary>
        void Execute();
        /// <summary>
        ///   Undoes a previously performed command.
        /// </summary>
        void Unexecute();
    }
}
