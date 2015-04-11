using MSG.IO;
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
    ///   Command design pattern.
    /// </summary>
    abstract public class Command
    {
        /// <summary>
        ///   Performs the command and somehow notes what was 
        ///   done so it can be possibly undone.
        /// </summary>
        /// <returns>
        ///   Zero on success, nonzero on failure.
        /// </returns>
        abstract public int Do(Print print, Read read);
        /// <summary>
        ///   Undoes a previously performed command.
        /// </summary>
        /// <returns>
        ///   Zero on success, nonzero on failure.
        /// </returns>
        virtual public int Undo(Print print, Read read)
        {
            throw new CannotUndoException();
        }
    }
}
