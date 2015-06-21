using MSG.Types.Array;
using MSG.Types.String;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MSG.IO
{
    public partial class Editor
    {
        /// <summary>
        ///   Manages the console state for the editor.
        /// </summary>
        public class View
        {
            /// <summary>
            ///   Text buffer.
            /// </summary>
            Buffer buffer;
            /// <summary>
            ///   Width of the console
            /// </summary>
            int consoleWidth;
            /// <summary>
            ///   The width of each line in the edit buffer
            /// </summary>
            EndlessArray<int> lineWidths;
            /// <summary>
            ///   Current console cursor position
            /// </summary>
            ConsolePos cursorPos;
            /// <summary>
            ///   The output object
            /// </summary>
            Print print;
            /// <summary>
            ///   The starting position of the console cursor
            /// </summary>
            /// <remarks>
            ///   public for what reason?
            /// </remarks>
            public readonly ConsolePos startCursorPos;
            /// <summary>
            ///   Calculates the positions of line breaks, cursor, etc.  This object
            ///   acts as a mediator for the view's interactions with the model. 
            /// </summary>
            /// <remarks>
            ///   public for what reason?
            /// </remarks>
            public WordWrapper wordWrapper;


            /// <summary>
            ///   Initializes the console state tracking class.
            /// </summary>
            /// <param name="buffer">
            ///   A reference to the editor buffer used to initialize
            ///   the WordWrapper object.  The buffer object is not
            ///   modified within this class or the WordWrapper class.
            /// </param>
            /// <param name="print">
            ///   All output is done through the print object
            /// </param>
            public View(Buffer buffer, Print print)
            {
                this.buffer = buffer;
                this.print = print;
                this.cursorPos = print.CursorPos;
                this.consoleWidth = print.BufferWidth;
                this.startCursorPos = print.CursorPos;
                this.lineWidths = new EndlessArray<int>(
                    this.consoleWidth - LineLeft(0, this.startCursorPos.left),
                    this.consoleWidth - LineLeft(1, this.startCursorPos.left)
                );
                this.wordWrapper = new WordWrapper(buffer, lineWidths);
            }

            /// <summary>
            ///   Handle backspace key.
            /// </summary>
            public void Backspace()
            {
                RedrawEditor();
            }

            public ConsolePos BufferPosToCursorPos(int bufferPos)
            {
                return EditorPosToCursorPos(wordWrapper.BufferPosToEditorPos(bufferPos));
            }

            /// <summary>
            ///   Moves the cursor to the end of the line and returns the
            ///   resulting buffer position.
            /// </summary>
            public int CursorEnd()
            {
                // find current line
                int currentLineIndex = cursorPos.top;
                // find end of current line
                int bufferPosOfEndOfCurrentLine = wordWrapper[currentLineIndex].EndIndex;
                // set bufferPos to end of current line
                buffer.Cursor = bufferPosOfEndOfCurrentLine;
                // update cursor to match bufferPos
                cursorPos = BufferPosToCursorPos(buffer.Cursor);
                SetCursorPos(cursorPos);
                // boom, done.
                return buffer.Cursor;
            }

            /// <summary>
            ///   Moves the cursor to the beginning of the line and returns
            ///   the resulting buffer position.
            /// </summary>
            public int CursorHome()
            {
                // find current line
                int currentLineIndex = cursorPos.top;
                // find start of current line
                int bufferPosOfStartOfCurrentLine = wordWrapper[currentLineIndex].StartIndex;
                // set bufferPos to start of current line
                buffer.Cursor = bufferPosOfStartOfCurrentLine;
                // update cursor to match bufferPos
                cursorPos = BufferPosToCursorPos(buffer.Cursor);
                SetCursorPos(cursorPos);
                // boom, done.
                return buffer.Cursor;
            }

            /// <summary>
            ///   Handles the left cursor key.
            /// </summary>
            public void CursorLeft()
            {
                cursorPos = BufferPosToCursorPos(buffer.Cursor);
                SetCursorPos(cursorPos);
            }

            /// <summary>
            ///   Handles the right cursor key.
            /// </summary>
            public void CursorRight()
            {
                cursorPos = BufferPosToCursorPos(buffer.Cursor);                
                SetCursorPos(cursorPos);
            }

            /// <summary>
            ///   Deletes the character at the cursor.
            /// </summary>
            public void Delete()
            {
                RedrawEditor();
            }

            /// <summary>
            ///   Converts an editor-relative cursor position to a console cursor position.
            /// </summary>
            /// <param name="editorPos">
            ///   Editor-relative cursor position
            /// </param>
            /// <returns>
            ///   Console cursor position
            /// </returns>
            public ConsolePos EditorPosToCursorPos(ConsolePos editorPos)
            {
                ConsolePos p;
                p.left = LineLeft(editorPos.top, startCursorPos.left) + editorPos.left;
                p.top = startCursorPos.top + editorPos.top;
                return p;
            }

            /// <summary>
            ///   Insert a character into the current line on the console, moving
            ///   other characters forward or breaking the line if necessary.
            /// </summary>
            public void Insert()
            {
                RedrawEditor();
            }

            /// <summary>
            ///   The cursor left coordinate of the start of line lineIndex.
            /// </summary>
            /// <param name="lineIndex">
            ///   Index of the line whose left coordinate to return
            /// </param>
            /// <returns>
            ///   Left coordinate of the given line
            /// </returns>
            public static int LineLeft(int lineIndex, int startLeft)
            {
                return lineIndex == 0 ? startLeft : 0;
            }

            /// <summary>
            ///   Updates the console when the text has changed.
            /// </summary>
            public void RedrawEditor()
            {
                // Hide cursor during redraw
                print.IsCursorVisible = false;
                // Start at the beginning
                SetCursorPos(startCursorPos);
                // Calculate word wrapping
                wordWrapper.Update();
                // Loop through each line returned by the word wrapper
                for (int i = 0; i < wordWrapper.Count; i++)
                {
                    // Get next word-wrapped line
                    string s = wordWrapper[i].ToString();
                    // Right pad to the edge of the window
                    int padLen = lineWidths[i] - s.Length;
                    // If this line is the last that has been scrolled into view, don't
                    // pad all the way to the right to avoid unneeded scrolling.
                    if (padLen > 0 && i == wordWrapper.LinesScrolledFromWrapping)
                    {
                        padLen--;
                    }
                    string padding = padLen == 0 ? String.Empty : Format.Padding(padLen);
                    print.String(s + padding);
                }
                // Erase the old line if the number of lines was reduced by the last operation
                if (wordWrapper.Dewrap)
                {
                    print.String(Format.Padding(lineWidths[wordWrapper.Count]
                        - (wordWrapper.Count == wordWrapper.LinesScrolledFromWrapping ? 1 : 0)));
                }
                // Position cursor with respect to wrapping
                cursorPos = BufferPosToCursorPos(buffer.Cursor);
                if (cursorPos.left >= consoleWidth)
                    throw new Exception("BufferPosToCursorPos() is broke");
                SetCursorPos(cursorPos);
                // Show cursor again
                print.IsCursorVisible = true;
            }

            /// <summary>
            ///   Prints a newline after the user is done typing.
            /// </summary>
            public void Return()
            {
                print.Newline();
            }

            /// <summary>
            ///   Sets the cursor position to coordinate (l, t).
            /// </summary>
            private void SetCursorPos(ConsolePos pos)
            {
                if (pos.left < 0 || pos.left >= consoleWidth)
                    throw new ArgumentOutOfRangeException("Invalid cursor left position: " + pos.left);
                if (pos.top < 0 /*|| pos.top >= consoleWidth*/)
                    throw new ArgumentOutOfRangeException("Invalid cursor top position: " + pos.top);
                print.CursorPos = pos;
            }
        }
    }
}
