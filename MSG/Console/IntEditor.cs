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
        ///   Initialize print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="read">Used to read the user input</param>
        public IntEditor(Print print, Read read)
            : base(print, read)
        {
        }

        /// <summary>
        ///   True if input is an integer.
        /// </summary>
        public override bool InputIsValid(string input)
        {
            return intRe.IsMatch(input);
        }

        public override bool KeyIsValid(ConsoleKeyInfo keyInfo)
        {
            return (keyInfo.Key >= ConsoleKey.D0 && keyInfo.Key <= ConsoleKey.D9)
                || keyInfo.Key == ConsoleKey.OemMinus;
        }

        /// <summary>
        ///   Prints a prompt and gets an int from the user
        /// </summary>
        /// <param name="promptMsg">The prompt</param>
        public int IntPrompt(string prompt = "# ")
        {
            string input = base.StringPrompt(prompt);
            return Convert.ToInt32(input);
        }
    }
}
