using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Patterns
{
    /// <summary>
    ///   Command design pattern.
    /// </summary>
    abstract public class Command
    {
        /// <summary>
        ///   Should prompt the user for any necessary parameters,
        ///   store them and perform the command.
        /// </summary>
        abstract public void Do();
        /// <summary>
        ///   Should be set by Do(), Redo(), and Undo() to relay
        ///   to the user information about what was last done.
        /// </summary>
        string message = "";
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        /// <summary>
        ///   Redoes a previously undone command.
        /// </summary>
        virtual public void Redo()
        {
            throw new NotSupportedException("This operation cannot be redone");
        }
        /// <summary>
        ///   Undoes a previously performed command.
        /// </summary>
        virtual public void Undo()
        {
            throw new NotSupportedException("This operation cannot be undone");
        }
    }
}
