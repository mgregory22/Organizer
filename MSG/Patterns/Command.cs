using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Patterns
{
    /// <summary>
    ///   Thrown when an attempt is made to undo a command that cannot be undone.
    /// </summary>
    [Serializable]
    public class CannotUndoException : NotSupportedException
    {
        public CannotUndoException() { }
        public CannotUndoException(string message) : base(message) { }
        public CannotUndoException(string message, Exception inner) : base(message, inner) { }
        protected CannotUndoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    /// <summary>
    ///   A basic Command design pattern.
    /// </summary>
    abstract public class Command
    {
        /// <summary>
        ///   Performs the command and somehow notes what was done so it can be possibly undone.
        /// </summary>
        abstract public void Do();
        /// <summary>
        ///   Undoes a previously performed command.
        /// </summary>
        virtual public void Undo()
        {
            throw new CannotUndoException();
        }
    }
}
