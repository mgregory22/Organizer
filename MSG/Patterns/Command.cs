using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Patterns
{
    /// <summary>
    ///   Thrown when an attempt is made to undo a command that
    ///   inherently cannot be undone.
    /// </summary>
    public class CannotUndoException : NotSupportedException
    {
        public CannotUndoException() { }
        public CannotUndoException(string message) : base(message) { }
        public CannotUndoException(string message, Exception inner) : base(message, inner) { }
    }
    /// <summary>
    ///   Command design pattern.
    /// </summary>
    abstract public class Command
    {
        /// <summary>
        ///   Should prompt the user for any necessary parameters,
        ///   store them and perform the command.
        /// </summary>
        /// <returns>
        ///   Zero on success, nonzero on failure.
        /// </returns>
        abstract public int Do();
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
        ///   Performs a previously undone command.
        /// </summary>
        /// <returns>
        ///   Zero on success, nonzero on failure.
        /// </returns>
        virtual public int Redo()
        {
            return 0;
        }
        /// <summary>
        ///   Undoes a previously performed command.
        /// </summary>
        /// <returns>
        ///   Zero on success, nonzero on failure.
        /// </returns>
        virtual public int Undo()
        {
            throw new CannotUndoException();
        }
    }
}
