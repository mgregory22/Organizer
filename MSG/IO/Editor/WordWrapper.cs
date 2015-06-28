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
        public struct LineBoundaries
        {
            public int startIndex, lineBreak;
            public string text;

            public override string ToString()
            {
                return text.Substring(startIndex, lineBreak - startIndex);
            }
        }

        public class WordWrapper
        {
            Editor.Buffer buffer;
            
            bool dewrap;
            
            /// <summary>
            ///   Marks the buffer position of the end of each line exclusive.  So a
            ///   first lineBreak of 6 would put the first char of the next line at
            ///   position 6 and the first line would be length 6.
            /// </summary>
            List<int> lineBreaks;

            int linesScrolledFromWrapping;

            EndlessArray<int> lineWidths;

            /// <summary>
            ///   Scans the given text with respect to the given window width
            ///   and determines the substring coordinates needed to wordwrap
            ///   the text in the window.
            /// </summary>
            /// <param name="buffer">
            ///   The memory buffer for the text being edited.  Should be
            ///   treated as read-only.  Do not modify this object within
            ///   this class.
            /// </param>
            /// <param name="lineWidths">
            ///   The maximum width of each line in characters.  If there are
            ///   more lines than there are entries in lineWidths, then the
            ///   last entry is used for subsequent lines.
            /// </param>
            public WordWrapper(Editor.Buffer buffer, EndlessArray<int> lineWidths)
            {
                this.buffer = buffer;
                this.dewrap = false;
                this.lineBreaks = new List<int>();
                this.lineWidths = lineWidths;
                this.linesScrolledFromWrapping = 0;
                Update();
            }

            public void AddLine(int lineEnd)
            {
                lineBreaks.Add(lineEnd);
                linesScrolledFromWrapping = Math.Max(linesScrolledFromWrapping, lineBreaks.Count - 1);
            }

            /// <summary>
            ///   Converts the position of a character in the buffer to a console cursor position.
            /// </summary>
            /// <param name="bufferPos">
            ///   Buffer position
            /// </param>
            /// <returns>
            ///   The (left, top) position within the editor (eg (0,0) is the first column on the
            ///   first line, etc)
            /// </returns>
            public ConsolePos BufferPosToEditorPos(int bufferPos)
            {
                int top = lineBreaks.FindIndex(
                    lineBreak => bufferPos < lineBreak
                );
                // If the cursor is sitting at the end of the text, then top will be the last line
                if (top == -1)
                {
                    top = lineBreaks.Count - 1;
                }
                ConsolePos editorPos;
                editorPos.left = bufferPos - LineStart(top);
                // Cursor wraps around if it's at the end of a full line
                if (editorPos.left == lineWidths[top])
                {
                    editorPos.left = 0;
                    top++;
                }
                editorPos.top = top;
                return editorPos;
            }

            public int Count
            {
                get { return lineBreaks.Count; }
            }

            /// <summary>
            ///   This is set to true when the last operation reduced the
            ///   number of lines to display (ie when the previously last
            ///   line needs to be erased).
            /// </summary>
            public bool Dewrap
            {
                get { return dewrap; }
            }

            public LineBoundaries GetLineBoundaries(int i)
            {
                LineBoundaries lb;
                lb.startIndex = LineStart(i);
                lb.lineBreak = lineBreaks[i];
                lb.text = buffer.Text;
                return lb;
            }

            public bool Is_i_AtAHardLineReturnChar(char charAtI)
            {
                return charAtI == '\n' || charAtI == '\r';
            }

            public bool Is_i_AtTheWindowBreak(int lineLen, char charAtI)
            {
                return lineLen == this.lineWidths[lineBreaks.Count];
            }

            /// <summary>
            ///   Returns true if the pointer at _i_ is on a word break
            ///   (ie char i is a space and char i-1 is a letter).
            /// </summary>
            /// <param name="charBeforeCursor">
            ///   Character before the cursor
            /// </param>
            /// <param name="charAtCursor">
            ///   Character at the cursor
            /// </param>
            /// <returns>
            ///   True if cursor is on a word break
            /// </returns>
            public bool Is_i_JustAfterAWord(char charBeforeI, char charAtI)
            {
                return !char.IsWhiteSpace(charBeforeI) && char.IsWhiteSpace(charAtI);
            }

            public bool Is_i_OnWordBeginning(char charBeforeI, char charAtI)
            {
                return char.IsWhiteSpace(charBeforeI) && !char.IsWhiteSpace(charAtI);
            }

            public bool IsLastLine(int lineIndex)
            {
                return lineIndex == lineBreaks.Count - 1;
            }

            /// <summary>
            ///   The maximum number of lines that were needed for editing, minus one
            /// </summary>
            public int LinesScrolledFromWrapping
            {
                get { return linesScrolledFromWrapping; }
            }

            /// <summary>
            ///   Returns the start of the line given by -index-.
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public int LineStart(int index)
            {
                // The start of the first line is zero.  The start of any other line
                // is the previous break.
                return index == 0 ? 0 : lineBreaks[index - 1];
            }

            /// <summary>
            ///   Returns the length of the buffer text.
            /// </summary>
            public int TextLength
            {
                get { return buffer.Text.Length; }
            }

            /// <summary>
            ///   Performs the word wrapping algorithm and creates
            ///   a list of word break positions for each extra line
            ///   that needs to be displayed.
            /// </summary>
            public void Update()
            {
                // Shortcuts
                string text = buffer.Text;
                int textLen = buffer.Text.Length;
                char charBeforeI = '\n';
                // Sanity check
                if (buffer.Cursor < 0 || buffer.Cursor > textLen)
                    throw new IndexOutOfRangeException(String.Format("Cursor cannot move outside text: Position {0}", buffer.Cursor));
                // Set dewrap flag at the end of this method if the user deleted 
                // enough characters to reduce the number of lines (ie the lost 
                // line needs to be erased).
                int previousLineCnt = lineBreaks.Count;
                // Clear previous calculations
                lineBreaks.Clear();
                // Start index of first line is always 0
                int lineStart = 0;
                // Save the cursor position of last scanned word break
                int lastWordBreak = 0;
                // Scan through all cursor positions
                for (int i = 0; i < textLen; i++)
                {
                    // Shortcut
                    char charAtI = text[i];
                    // _i_ is at the beginning of a word?
                    if (Is_i_OnWordBeginning(charBeforeI, charAtI))
                    {
                        // Save word break position
                        lastWordBreak = i;
                    }
                    // Length of line up to _i_
                    int lineLen = i - lineStart;
                    // _i_ is at a hard line return?
                    if (Is_i_AtAHardLineReturnChar(charAtI))
                    {
                        // Advance cursor past line return
                        i = Scan.SkipHardReturn(text, i);
                        // Break line for hard return
                        AddLine(i);
                        // Next bol
                        lineStart = i;
                        // Save word break position
                        lastWordBreak = lineStart;
                    }
                    // Reached window break?
                    else if (Is_i_AtTheWindowBreak(lineLen, charAtI))
                    {
                        // Found word break during scan?
                        if (lastWordBreak > lineStart)
                        {
                            // Back up cursor to last word break
                            i = lastWordBreak;
                            // Set eol at word break
                            lineLen = i - lineStart;
                        }
                        // Add the line indexes
                        AddLine(i);
                        // Next bol
                        lineStart = i;
                        // Save word break position
                        lastWordBreak = lineStart;
                    }
                    charBeforeI = charAtI;
                }
                // Add the last line, whatever it is
                AddLine(textLen);
                // If the cursor wrapped around but no text did, then there is
                // another scrolled line.
                if (textLen - lineStart == lineWidths[lineBreaks.Count - 1])
                {
                    linesScrolledFromWrapping++;
                }
                // Update dewrap
                dewrap = lineBreaks.Count < previousLineCnt;
            }
        }
    }
}
