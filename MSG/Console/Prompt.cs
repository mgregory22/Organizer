using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    public class Prompt
    {
        protected Print print;
        protected string promptMsg;
        protected Read read;
        /// <summary>
        ///   The object used to display information to the user.
        /// </summary>
        public Print Print
        {
            get { return print; }
            set { print = value; }
        }
        /// <summary>
        ///   Initialize a prompt with message, print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="promptMsg">The prompt string to use when requesting user input</param>
        /// <param name="read">Used to read the user input</param>
        public Prompt(Print print, string promptMsg, Read read)
	    {
            this.print = print;
            this.promptMsg = promptMsg;
            this.read = read;
	    }
        /// <summary>
        ///   The text that prompts the user for input.
        /// </summary>
        public string PromptMsg
        {
            get { return promptMsg; }
            set { promptMsg = value; }
        }
        /// <summary>
        ///   The object used to get user input.
        /// </summary>
        public Read Read
        {
            get { return read; }
            set { read = value; }
        }
    }
}
