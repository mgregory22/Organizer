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
            ///   Current cursor position
            /// </summary>
            ConsolePos cursorPos;
            /// <summary>
            ///   The output object
            /// </summary>
            Print print;
            /// <summary>
            ///   The cursor position of the first character of the edited text
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

            public ConsolePos BufferPointToCursorPos(int point)
            {
                return EditorPosToCursorPos(wordWrapper.BufferPointToEditorPos(point));
            }

            /// <summary>
            ///   Moves the cursor to the end of the line and returns the
            ///   resulting buffer point.
            /// </summary>
            public int CursorEnd()
            {
                ConsolePos editorPos = CursorPosToEditorPos(cursorPos);
                // find current line
                int currentLineIndex = editorPos.top;
                // find end of current line
                int endPointOfCurrentLine = wordWrapper.GetLineBreak(currentLineIndex)
                    - (wordWrapper.IsLastLine(currentLineIndex) ? 0 : 1);
                // set bufferPos to end of current line
                buffer.Point = endPointOfCurrentLine;
                // update cursor to match bufferPos
                cursorPos = BufferPointToCursorPos(endPointOfCurrentLine);
                SetCursorPos(cursorPos);
                // boom, done.
                return buffer.Point;
            }

            /// <summary>
            ///   Moves the cursor to the beginning of the line and returns
            ///   the resulting buffer position.
            /// </summary>
            public int CursorHome()
            {
                ConsolePos editorPos = CursorPosToEditorPos(cursorPos);
                // find current line
                int currentLineIndex = editorPos.top;
                // find start of current line
                int startPointOfCurrentLine = wordWrapper.GetLineStart(currentLineIndex);
                // set bufferPos to start of current line
                buffer.Point = startPointOfCurrentLine;
                // update cursor to match bufferPos
                cursorPos = BufferPointToCursorPos(startPointOfCurrentLine);
                SetCursorPos(cursorPos);
                // boom, done.
                return buffer.Point;
            }

            public ConsolePos CursorPosToEditorPos(ConsolePos cursorPos)
            {
                ConsolePos editorPos;
                editorPos.left = cursorPos.left;
                editorPos.top = cursorPos.top - startCursorPos.top;
                return editorPos;
            }

            /// <summary>
            ///   Converts an editor-relative cursor position to a position relative
            ///   to the whole console window.
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
            ///   Moves the cursor to the end of the input and prints a newline
            ///   (for after the user is done typing).
            /// </summary>
            public void ExitEditor()
            {
                cursorPos.left = 0;
                cursorPos.top = startCursorPos.top + wordWrapper.Count - 1;
                SetCursorPos(cursorPos);
                print.Newline();
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
                    string s = wordWrapper.GetLine(i);
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
                cursorPos = BufferPointToCursorPos(buffer.Point);
                SetCursorPos(cursorPos);
                // Show cursor again
                print.IsCursorVisible = true;
            }

            /// <summary>
            ///   Sets the cursor position to coordinate (l, t).
            /// </summary>
            private void SetCursorPos(ConsolePos pos)
            {
                if (pos.left < 0 || pos.left >= consoleWidth)
                    throw new ArgumentOutOfRangeException("Invalid cursor left position: " + pos.left);
                if (pos.top < 0 /*|| pos.top >= consoleHeight*/)
                    throw new ArgumentOutOfRangeException("Invalid cursor top position: " + pos.top);
                print.CursorPos = pos;
            }

            /// <summary>
            ///   Moves the console cursor to the position corresponding
            ///   to the buffer insertion point.
            /// </summary>
            public void UpdateCursor()
            {
                cursorPos = BufferPointToCursorPos(buffer.Point);
                SetCursorPos(cursorPos);
            }
        }
    }
}
