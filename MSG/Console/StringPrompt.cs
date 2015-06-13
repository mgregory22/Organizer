using MSG.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    public class StringPrompt : Prompt
    {
        public StringPrompt(Print print, string promptMsg, Read read)
            : base(print, promptMsg, read)
        {

        }

        /// <summary>
        ///   Displays the prompt and reads a string.
        /// </summary>
        /// <returns>The string entered by the user</returns>
        public string DoPrompt()
        {
            string s;
            do
            {
                PrintPrompt();
                s = Read.String();
            } while (StringIsInvalid(s));
            return s;
        }

        private bool StringIsInvalid(string s)
        {
            return false;
        }
    }
}
