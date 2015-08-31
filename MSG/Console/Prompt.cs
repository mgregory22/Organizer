//
// MSG/Console/Prompt.cs
//

using MSG.IO;

namespace MSG.Console
{
    public class Prompt
    {
        protected Print print;
        protected string promptMsg;
        protected Read read;

        /// <summary>
        ///   Initialize a prompt with message, print and read objects.
        /// </summary>
        /// <param name="print">Used to print the prompt</param>
        /// <param name="promptMsg">The prompt string to use when requesting user input</param>
        /// <param name="read">Used to read the user input</param>
        public Prompt(Print print, Read read, string promptMsg)
        {
            this.print = print;
            this.read = read;
            this.promptMsg = promptMsg;
        }

        /// <summary>
        ///   Wait for key to keep the window open.
        /// </summary>
        public void Pause()
        {
            print.String("Press a key");
            read.GetNextChar(true);
        }

        /// <summary>
        ///   The object used to display information to the user.
        /// </summary>
        public Print Print
        {
            get { return print; }
            set { print = value; }
        }

        /// <summary>
        ///   Uses the print object to print the prompt message (without newline).
        /// </summary>
        public void PrintPrompt()
        {
            Print.String(promptMsg);
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
