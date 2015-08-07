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
            class OutputDiff
            {
                public int point;
                public int length;
                public OutputDiff(int point, int length)
                {
                    this.point = point;
                    this.length = length;
                }
            }

            /// <summary>
            ///   What's currently displayed on the console
            /// </summary>
            string consoleState;
            /// <summary>
            ///   Width of the console
            /// </summary>
            int consoleWidth;
            /// <summary>
            ///   Current cursor position
            /// </summary>
            ConsolePos cursorPos;
            /// <summary>
            ///   The width of each line in the edit buffer
            /// </summary>
            EndlessArray<int> lineWidths;
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
                this.print = print;
                this.cursorPos = print.CursorPos;
                this.consoleState = "";
                this.consoleWidth = print.BufferWidth;
                this.startCursorPos = print.CursorPos;
                this.lineWidths = new EndlessArray<int>(
                    this.consoleWidth - LineLeft(0, this.startCursorPos.left),
                    this.consoleWidth - LineLeft(1, this.startCursorPos.left)
                );
                this.wordWrapper = new WordWrapper(buffer.Text, lineWidths);
            }

            public ConsolePos BufferPointToCursorPos(int point)
            {
                return EditorPosToCursorPos(wordWrapper.BufferPointToEditorPos(point));
            }

            public ConsolePos BufferPointToUnwrappedCursorPos(int point)
            {
                ConsolePos pos;
                pos.left = (point + startCursorPos.left) % consoleWidth;
                pos.top = startCursorPos.top + (point + startCursorPos.left) / consoleWidth;
                return pos;
            }

            /// <summary>
            ///   Moves the cursor down one line.
            /// </summary>
            /// <param name="point">
            ///   The position of the current point, which is returned if
            ///   the cursor cannot move down.
            /// </param>
            /// <param name="count">
            ///   Number of lines to move cursor down
            /// </param>
            /// <returns>
            ///   The buffer point in the next line at the same
            ///   horizontal position (if possible)
            /// </returns>
            public int CursorDown(int point, int count = 1)
            {
                ConsolePos editorPos = CursorPosToEditorPos(cursorPos);
                // find current column
                int column = editorPos.left;
                // find current line index
                int line = editorPos.top;
                // if moving down is possible
                if (!wordWrapper.IsLastLine(line))
                {
                    // move down one
                    int nextLine = line + 1;
                    // find start of next line
                    int startPointOfNextLine = wordWrapper.GetLineStart(nextLine);
                    // try to set the same column number, but the new line
                    // could be too short
                    int endPointOfNextLine = wordWrapper.GetLineBreak(nextLine);
                    int newPoint = Math.Min(startPointOfNextLine + column, endPointOfNextLine);
                    return SetCursorByPoint(newPoint);
                }
                // return the current point if the cursor can't move down
                return point;
            }

            /// <summary>
            ///   Moves the cursor to the end of the line.
            /// </summary>
            /// <returns>
            ///   The buffer point at the end of the current line
            /// </returns>
            public int CursorEnd()
            {
                ConsolePos editorPos = CursorPosToEditorPos(cursorPos);
                // find current line index
                int line = editorPos.top;
                // find end of current line
                int endPointOfLine = wordWrapper.GetLineBreak(line)
                    - (wordWrapper.IsLastLine(line) ? 0 : 1);
                // set point to end of line and update cursor to match
                return SetCursorByPoint(endPointOfLine);
            }

            /// <summary>
            ///   Moves the cursor to the beginning of the line.
            /// </summary>
            /// <returns>
            ///   The buffer point at the start of the current line
            /// </returns>
            public int CursorHome()
            {
                ConsolePos editorPos = CursorPosToEditorPos(cursorPos);
                // find current line index
                int line = editorPos.top;
                // find start of current line in the buffer
                int startPointOfLine = wordWrapper.GetLineStart(line);
                // set point to start of line and update cursor to match
                return SetCursorByPoint(startPointOfLine);
            }

            /// <summary>
            ///   Moves the cursor left one column unless it wraps or it's
            ///   at the beginning.
            /// </summary>
            /// <param name="point">
            ///   Original cursor position
            /// </param>
            /// <param name="count">
            ///   Number of columns to move cursor left
            /// </param>
            /// <returns>
            ///   The new cursor position
            /// </returns>
            public int CursorLeft(int point, int count = 1)
            {
                return point; // TODO
            }

            /// <summary>
            ///   Moves the cursor up one line.
            /// </summary>
            /// <param name="point">
            ///   The position of the current point, which is returned if
            ///   the cursor cannot move up.
            /// </param>
            /// <param name="count">
            ///   Number of lines to move cursor up
            /// </param>
            /// <returns>
            ///   If possible, the buffer point in the previous line at
            ///   the same horizontal position.  Otherwise, the current
            ///   point.
            /// </returns>
            public int CursorUp(int point, int count = 1)
            {
                ConsolePos editorPos = CursorPosToEditorPos(cursorPos);
                // find current column
                int column = editorPos.left;
                // find current line index
                int line = editorPos.top;
                // if moving up is possible
                if (!wordWrapper.IsFirstLine(line))
                {
                    // move up one
                    int prevLine = line - 1;
                    // find start in buffer of previous line
                    int startPointOfPrevLine = wordWrapper.GetLineStart(prevLine);
                    // try to set the same column number
                    int newColumn = Math.Max(startPointOfPrevLine + column - LineLeft(prevLine, startCursorPos.left), 0);
                    // the new line could be too short
                    int endPointOfPrevLine = wordWrapper.GetLineBreak(prevLine) - 1;
                    int newPoint = Math.Min(newColumn, endPointOfPrevLine);
                    return SetCursorByPoint(newPoint);
                }
                // return the current point if the cursor can't move up
                return point;
            }

            /// <summary>
            ///   Converts an absolute cursor position to a position relative
            ///   to the editor.
            /// </summary>
            /// <param name="cursorPos">
            ///   Absolute cursor position
            /// </param>
            /// <returns>
            ///   Editor-relative cursor position
            /// </returns>
            public ConsolePos CursorPosToEditorPos(ConsolePos cursorPos)
            {
                ConsolePos editorPos;
                editorPos.left = cursorPos.left;
                editorPos.top = cursorPos.top - startCursorPos.top;
                return editorPos;
            }

            /// <summary>
            ///   Creates a list of differences between a new string and an old
            ///   string.
            /// </summary>
            /// <param name="newStr">
            ///   New string
            /// </param>
            /// <param name="oldStr">
            ///   Old string
            /// </param>
            /// <returns>
            ///   List of diffs to the old string that change it to the new
            ///   string
            /// </returns>
            private List<OutputDiff> DiffStrings(string newStr, string oldStr)
            {
                List<OutputDiff> outputDiffs = new List<OutputDiff>();
                int scanLen = Math.Min(newStr.Length, oldStr.Length);
                bool inChangedBlock = false;
                int changeStart = 0;
                for (int i = 0; i < scanLen; i++)
                {
                    if (inChangedBlock)
                    {
                        if (newStr[i] == oldStr[i])
                        {
                            // End of changed block: add the changed block to the list
                            outputDiffs.Add(
                                new OutputDiff(
                                    changeStart
                                  , i - changeStart
                            ));
                            inChangedBlock = false;
                        }
                    }
                    else
                    {
                        if (newStr[i] != oldStr[i])
                        {
                            
                            // Beginning of changed block: mark the start
                            changeStart = i;
                            inChangedBlock = true;
                        }
                    }
                }
                if (newStr.Length > scanLen)  // eg adding a character to the end of the input
                {
                    if (!inChangedBlock)
                    {
                        changeStart = scanLen;
                    }
                    outputDiffs.Add(new OutputDiff(
                        changeStart
                      , newStr.Length - changeStart
                    ));
                }
                else if (oldStr.Length > scanLen)  // eg backspacing at the end of the input
                {
                    if (!inChangedBlock)
                    {
                        changeStart = scanLen;
                    }
                    outputDiffs.Add(
                        new OutputDiff(
                            changeStart
                          , (scanLen - changeStart) + (oldStr.Length - scanLen)
                    ));
                }
                else  // eg replacing a character at the end of the input
                {
                    if (inChangedBlock)
                    {
                        outputDiffs.Add(new OutputDiff(
                            changeStart
                          , newStr.Length - changeStart
                        ));
                    }
                }
                return outputDiffs;
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
            ///   Prints each string at the associated position in the given list.
            /// </summary>
            /// <param name="outputDiffs">
            ///   List of ((left, top), text) tuples
            /// </param>
            private void PrintDiffs(string newStr, List<OutputDiff> outputDiffs)
            {
                foreach (OutputDiff diff in outputDiffs)
                {
                    SetCursorPos(BufferPointToUnwrappedCursorPos(diff.point));
                    int diffEnd = diff.point + diff.length;
                    if (diff.point < newStr.Length && diffEnd < newStr.Length)
                    {
                        print.String(newStr.Substring(diff.point, diff.length));
                    }
                    else if (diff.point < newStr.Length)
                    {
                        print.String(newStr.Substring(diff.point, newStr.Length - diff.point));
                        print.String(new String(' ', diffEnd - newStr.Length));
                    }
                    // diffEnd < diff.point is impossible
                    else // diff.point >= newStr.Length && diffEnd >= newStr.Length
                    {
                        print.String(new String(' ', diffEnd - diff.point));
                    }
                }
            }

            /// <summary>
            ///   Updates the console when the text has changed.
            /// </summary>
            public void RedrawEditor(string text, int point)
            {
                // Calculate word wrapping
                wordWrapper.Update(text);
                // Buffer used to compare with the console state in order to calculate the
                // minimal number of console writes
                StringBuilder newState = new StringBuilder();
                // Loop through each line returned by the word wrapper
                for (int i = 0; i < wordWrapper.Count - 1; i++)
                {
                    // Right pad to the edge of the window to erase all characters and
                    // incidentally eliminate the need for newlines
                    int padLen = lineWidths[i] - wordWrapper.GetLineLen(i);
                    // If this line is the last that has been scrolled into view, don't
                    // pad all the way to the right to avoid unneeded scrolling.
                    if (padLen > 0 && i == wordWrapper.LinesScrolledFromWrapping)
                    {
                        padLen--;
                    }
                    string padding = padLen == 0 ? String.Empty : Format.Padding(padLen);
                    newState.Append(wordWrapper.GetLine(text, i) + padding);
                }
                newState.Append(wordWrapper.GetLine(text, wordWrapper.Count - 1));
                // Erase the old line if the number of lines was reduced by the last operation
                //if (wordWrapper.Dewrap)
                //{
                //    newState.Append(Format.Padding(lineWidths[wordWrapper.Count]
                //        - (wordWrapper.Count == wordWrapper.LinesScrolledFromWrapping ? 1 : 0)));
                //}
                // Print everything
                string newStr = newState.ToString();
                // Compare newState to consoleState to create diff list of
                // (cursorLeft, cursorTop, string) tuples.
                List<OutputDiff> outputDiffs = DiffStrings(newStr, consoleState);
                // Hide cursor during redraw
                print.IsCursorVisible = false;
                // Print each diff in list
                PrintDiffs(newStr, outputDiffs);
                // Position cursor with respect to wrapping
                cursorPos = BufferPointToCursorPos(point);
                SetCursorPos(cursorPos);
                // Show cursor again
                print.IsCursorVisible = true;
                // Save the console state
                consoleState = newStr.TrimEnd(' ');
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
                if (pos.left != print.CursorLeft || pos.top != print.CursorTop)
                {
                    print.CursorPos = pos;
                }
            }

            /// <summary>
            ///   Sets the cursor position by the given buffer insertion point.
            /// </summary>
            /// <param name="point">
            ///   Point
            /// </param>
            /// <returns>
            ///   Point
            /// </returns>
            private int SetCursorByPoint(int point)
            {
                // update cursor to match bufferPos
                cursorPos = BufferPointToCursorPos(point);
                SetCursorPos(cursorPos);
                // return point just for convenience
                return point;
            }

            /// <summary>
            ///   Moves the console cursor to the position corresponding
            ///   to the buffer insertion point.
            /// </summary>
            public void UpdateCursor(int point)
            {
                cursorPos = BufferPointToCursorPos(point);
                SetCursorPos(cursorPos);
            }
        }
    }
}
