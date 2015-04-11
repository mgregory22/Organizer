using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.IO
{
    /// <summary>
    ///   Encapsulates the raw printing to the console.  This was intended
    ///   to be easy to derive from and override its methods for testing
    ///   and for printing to other devices and situations.
    /// </summary>
    public class Print
    {
        /// <summary>
        ///   Prints a char to the console.
        /// </summary>
        /// <param name="c">Char to print</param>
        /// <param name="nl">Prints newline if true</param>
        virtual public void Char(char c, bool nl = false)
        {
            if (nl)
                System.Console.WriteLine(c);
            else
                System.Console.Write(c);
        }
        /// <summary>
        ///   Prints one or more newlines to the console.
        /// </summary>
        virtual public void Newline(int n = 1)
        {
            for (int i = 0; i < n; i++) System.Console.WriteLine();
        }
        /// <summary>
        ///   Prints a string to the console.
        /// </summary>
        /// <param name="s">String to print</param>
        /// <param name="nl">Prints newline if true</param>
        virtual public void String(string s, bool nl = false)
        {
            if (nl)
                System.Console.WriteLine(s);
            else
                System.Console.Write(s);
        }
    }
}
