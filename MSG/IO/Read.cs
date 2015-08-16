using MSG.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.IO
{
    /// <summary>
    ///   Encapsulates the raw reading from the console.  This was intended
    ///   to be easy to derive from and override its methods for testing
    ///   and for reading from other devices and situations.
    /// </summary>
    public class Read
    {
        protected Print print;

        /// <summary>
        ///   The read object needs the ability to output the stuff read.
        /// </summary>
        /// <param name="print">
        ///   Everything read will be printed with this object, unless that is
        ///   disabled by setting print to null.
        /// </param>
        public Read(Print print)
        {
            this.print = print;
        }

        /// <summary>
        ///   Reads a character from the console (immediately, without the user
        ///   hitting enter).
        /// </summary>
        /// <returns>Key the user typed.</returns>
        virtual public char Char()
        {
            ConsoleKeyInfo key = System.Console.ReadKey(true);
            if (print != null) print.Char(key.KeyChar);
            return key.KeyChar;
        }

        /// <summary>
        ///   Reads a key from the console (immediately, without the user
        ///   hitting enter).
        /// </summary>
        /// <returns>Key the user typed.</returns>
        virtual public ConsoleKeyInfo Key()
        {
            ConsoleKeyInfo key = System.Console.ReadKey(true);
            if (print != null) print.Char(key.KeyChar);
            return key;
        }

        /// <summary>
        ///   Reads a line of text from the console.
        /// </summary>
        /// <returns>String the user typed.</returns>
        virtual public string String()
        {
            // The Editor object needs control of the printing, so let
            // the Editor own the print object during editing, and
            // disable automatic printing in this read object.
            Print tempPrint = print;
            print = null;
            Editor editor = new Editor(tempPrint, this);
            string input = editor.GetInput();
            print = tempPrint;
            return input;
        }
    }
}
