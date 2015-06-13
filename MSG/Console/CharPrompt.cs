using MSG.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    /// <summary>
    ///   Prompts the users for a char (without having to hit enter) until
    ///   the char passes validation.
    /// </summary>
    /// <remarks>
    ///   I really hate this class because all the methods are called "Prompt"
    /// </remarks>
    public class CharPrompt : Prompt
    {
        private char[] validList;

        /// <summary>
        ///   Displays the prompt and reads a character.
        /// </summary>
        /// <returns>The char entered by the user</returns>
        public char DoPrompt()
        {
            char c;
            do {
                PrintPrompt();
                c = Read.Char();
                Print.Newline();
            } while (CharIsInvalid(c));
            return c;
        }

        /// <summary>
        ///   Does all validation on the char.
        /// </summary>
        /// <param name="c">Char to validate</param>
        /// <returns>True if char is invalid</returns>
        private bool CharIsInvalid(char c)
        {
            if (validList != null && !validList.Contains(c))
            {
                Print.StringNL("Invalid selection. Try again.");
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
        public CharPrompt(Print print, string promptMsg, Read read)
            : base(print, promptMsg, read)
        {
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
