using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    /// <summary>
    ///   Encapsulates the raw reading from the console.  This was intended
    ///   to be easy to derive from and override its methods for testing
    ///   and for reading from other devices and situations.
    /// </summary>
    /// <remarks>
    ///   Do not print anything from this class.  All printing must occur
    ///   through a Print object.
    /// </remarks>
    public class Read
    {
        /// <summary>
        ///   Reads a key from the console (immediately, without the user
        ///   hitting enter).
        /// </summary>
        /// <returns>Key the user typed</returns>
        virtual public char Key()
        {
            ConsoleKeyInfo key;
            key = System.Console.ReadKey(true); // suppress printing
            return key.KeyChar;
        }
    }
}
