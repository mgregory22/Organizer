//
// MSG/Console/Editor.cs
//

using MSG.IO;
using System;

namespace MSG.Console
{
    /// <summary>
    ///   More featureful console input editor.
    /// </summary>
    public partial class Editor
    {
        protected Buffer buffer;
        protected Print print;
        protected string promptMsg;
        protected Read read;
        protected View view;

        public Editor(Print print, Read read, string promptMsg)
        {
            this.print = print;
            this.promptMsg = promptMsg;
            this.read = read;
            this.buffer = new Buffer();
        }

        /// <summary>
        ///   Gets one or more lines of input from the user.
        /// </summary>
        /// <param name="print">
        ///   Object used for raw printing
        /// </param>
        /// <param name="read">
        ///   Object used for raw reading
        /// </param>
        public string GetAndProcessKeys()
        {
            bool done = false;
            ConsoleKeyInfo keyInfo;
            while (!done)
            {
                keyInfo = read.GetNextKey();
                done = ProcessKey(keyInfo, buffer, view);
            }
            return buffer.ToString();
        }

        /// <summary>
        ///   Override that allows heir to provide custom validation
        ///   method for when the user presses enter
        /// </summary>
        /// <param name="input">
        ///   Complete input the user entered in
        /// </param>
        virtual public bool InputIsValid(string input)
        {
            return true;
        }

        /// <summary>
        ///   Override that allows heir to provide custom validation
        ///   method for each keystroke
        /// </summary>
        /// <param name="keyInfo">
        ///   Last keystroke entered by the user within the input loop
        /// </param>
        virtual public bool KeyIsValid(ConsoleKeyInfo keyInfo)
        {
            return true;
        }

        /// <summary>
        ///   Displays the prompt and reads a string.
        /// </summary>
        /// <returns>
        ///   The string entered by the user
        /// </returns>
        public string PromptAndInput()
        {
            string s;
            do
            {
                PrintPrompt();
                s = GetAndProcessKeys();
            } while (!InputIsValid(s));
            return s;
        }

        virtual public void PrintPrompt()
        {
            print.String(promptMsg);
            // The view has an internal startCursorPos property
            // that needs to be set after the prompt is printed.
            this.view = new View(buffer, print);
        }

        /// <summary>
        ///   Performs the key command action
        /// </summary>
        /// <param name="keyInfo">
        ///   Key command/input
        /// </param>
        /// <param name="buffer">
        ///   Editor buffer
        /// </param>
        /// <param name="view">
        ///   Editor console
        /// </param>
        /// <returns>
        ///   True if the user quit
        /// </returns>
        virtual public bool ProcessKey(ConsoleKeyInfo keyInfo, Buffer buffer, View view)
        {
            bool done = false;

            if (IsPrintable(keyInfo))
            {
                if (KeyIsValid(keyInfo))
                {
                    // insert any of the printable keys
                    buffer.Insert(keyInfo.KeyChar);
                    view.RedrawEditor(buffer.Text, buffer.Point);
                }
            }
            else if (IsShiftEnter(keyInfo))
            {
                buffer.Insert('\n');
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsBackspace(keyInfo))
            {
                buffer.RetreatPoint();
                buffer.Delete();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsCursorDown(keyInfo))
            {
                int point = view.CursorDown(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsCursorLeft(keyInfo))
            {
                int point = view.CursorLeft(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsCursorRight(keyInfo))
            {
                int point = view.CursorRight(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsCursorUp(keyInfo))
            {
                int point = view.CursorUp(buffer.Point);
                buffer.MovePoint(point);
            }
            else if (IsDelete(keyInfo))
            {
                buffer.Delete();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsEnd(keyInfo))
            {
                int point = view.CursorEnd();
                buffer.MovePoint(point);
            }
            else if (IsEnter(keyInfo))
            {
                // ignore enter on empty line
                if (!buffer.IsEmpty())
                {
                    view.ExitEditor();
                    done = true;
                }
            }
            else if (IsEscape(keyInfo))
            {
                buffer.Clear();
                view.ExitEditor();
                done = true;
            }
            else if (IsHome(keyInfo))
            {
                int point = view.CursorHome();
                buffer.MovePoint(point);
            }
            else if (IsWordLeft(keyInfo))
            {
                buffer.WordBack();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsWordRight(keyInfo))
            {
                buffer.WordForward();
                view.RedrawEditor(buffer.Text, buffer.Point);
            }
            else if (IsPause(keyInfo))
            {
                done = true;
            }
            else
            {
                // Ignore everything else
            }
            return done;
        }
    }
}
