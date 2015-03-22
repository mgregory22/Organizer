using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    /// <summary>
    ///   Prompts the users for a keystroke (without having to hit enter) until
    ///   the keystroke passes validation.
    /// </summary>
    /// <remarks>
    ///   I really hate this class because all the methods are called "Prompt"
    /// </remarks>
    public class KeyPrompt : Prompt
    {
        private char[] validList;
        /// <summary>
        ///   Displays the prompt and reads a keystroke.
        /// </summary>
        /// <returns>The char entered by the user</returns>
        public char DoPrompt()
        {
            char c;
            do {
                PrintPrompt();
                c = Read.Key();
                Print.Char(c, true);
            } while (KeyIsInvalid(c));
            return c;
        }
        /// <summary>
        ///   Does all validation on the key.
        /// </summary>
        /// <param name="c">Key to validate</param>
        /// <returns>True if key is invalid</returns>
        private bool KeyIsInvalid(char c)
        {
            if (validList != null && !validList.Contains(c))
            {
                Print.String("Invalid selection. Try again.", true);
                return true;
            }
            return false;
        }
        /// <summary>
        ///   Initialize a prompt with message, print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="promptMsg">The prompt string to use when requesting user input</param>
        /// <param name="read">Used to read the user input</param>
        public KeyPrompt(Print print, string promptMsg, Read read)
            : base(print, promptMsg, read)
        {
        }
        /// <summary>
        ///   Prints the prompt message (without newline).
        /// </summary>
        public void PrintPrompt()
        {
            Print.String(promptMsg);
        }
        /// <summary>
        ///   If this property is set, the key the user enters must be
        ///   in this list, otherwise an error message will be displayed
        ///   and the user reprompted.
        /// </summary>
        public char[] ValidList
        {
            get { return validList; }
            set { validList = value; }
        }
    }
}
