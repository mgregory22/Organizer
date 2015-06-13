using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Types.String
{
    /// <summary>
    ///   String scanning utility procedures
    /// </summary>
    public class Scan
    {
        /// <summary>
        ///   Given that text[i] is on a newline character, returns
        ///   the index of the next character that is not that newline.
        ///   If text[i] is not on a newline, it returns i.
        /// </summary>
        public static int SkipHardReturn(string text, int i)
        {
            // Skip only a single newline.
            // Works whether the newline is \n, \r, \r\n, or \n\r
            bool newlineSkipped = false;
            if (i == text.Length)
                return i;
            if (text[i] == '\n')
            {
                i++;
                if (i == text.Length)
                    return i;
                newlineSkipped = true;
            }
            if (text[i] == '\r')
            {
                i++;
                if (i == text.Length)
                    return i;
            }
            if (!newlineSkipped && text[i] == '\n')
            {
                i++;
                if (i == text.Length)
                    return i;
            }
            return i;
        }

        /// <summary>
        ///   Given that text[i] is on a whitespace character, returns
        ///   the index of the next character that is not whitespace.
        /// </summary>
        public static int SkipWhiteSpace(string text, int i)
        {
            if (i == text.Length)
                return i;
            while (char.IsWhiteSpace(text[i]))
            {
                i++;
                if (i == text.Length)
                    return i;
            }
            return i;
        }

    }
}
