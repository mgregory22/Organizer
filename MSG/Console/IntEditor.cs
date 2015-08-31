//
// MSG/Console/IntEditor.cs
//

using MSG.IO;
using System;
using System.Text.RegularExpressions;

namespace MSG.Console
{
    public class IntEditor : Editor
    {
        protected static Regex intRe = new Regex(@"^\s*-?[0-9]+$");

        /// <summary>
        ///   Initialize a prompt with message, print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="promptMsg">The prompt string to use when requesting user input</param>
        /// <param name="read">Used to read the user input</param>
        public IntEditor(Print print, Read read, string promptMsg)
            : base(print, read, promptMsg)
        {
        }

        override public bool InputIsValid(string input)
        {
            return intRe.IsMatch(input);
        }

        override public bool KeyIsValid(ConsoleKeyInfo keyInfo)
        {
            return (keyInfo.Key >= ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9)
                || keyInfo.Key == ConsoleKey.OemMinus;
        }
    }
}
