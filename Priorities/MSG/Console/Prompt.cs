using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    public class Prompt
    {
        private string promptMsg;
        /// <summary>
        ///   Prints the prompt to the console.
        /// </summary>
        virtual public void PrintPrompt()
        {
            System.Console.Write(promptMsg);
        }
        /// <summary>
        ///   Initialize a prompt with message and data type.
        /// </summary>
        /// <param name="promptMsg"></param>
        public Prompt(string promptMsg)
        {
            this.promptMsg = promptMsg;
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
        ///   Displays the prompt and reads an integer from the console.
        /// </summary>
        /// <returns></returns>
        virtual public int ReadInt()
        {
            int i = 0;
            return i;
        }
        /// <summary>
        ///   Displays the prompt and reads a keystroke from the console.
        /// </summary>
        /// <returns></returns>
        virtual public ConsoleKey ReadKey()
        {
            ConsoleKeyInfo key;
            PrintPrompt();
            key = System.Console.ReadKey(false);
            return key.Key;
        }
    }
}
