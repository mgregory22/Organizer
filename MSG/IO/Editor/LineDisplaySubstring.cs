using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.IO
{
    public partial class Editor
    {
        /// <summary>
        ///   (start, length) pair used for drawing wrapped lines on the console.
        ///   start is an index into a text buffer and length is number of chars to draw.
        /// </summary>
        public class LineDisplaySubstring
        {
            int length;
            int startIndex;
            string text;

            /// <summary>
            ///   A computed property equalling lineStart + lineLength
            /// </summary>
            public int EndIndex
            {
                get { return StartIndex + Length; }
                set
                {
                    if (value < StartIndex)
                        throw new IndexOutOfRangeException("Cannot set line end before line start");
                    Length = EndIndex - StartIndex;
                }
            }

            /// <summary>
            ///   Creates a (0, 0) pair.
            /// </summary>
            public LineDisplaySubstring(string text)
            {
                this.text = text;
                StartIndex = Length = 0;
            }

            /// <summary>
            ///   Creates a (lineStart, 0) pair.
            /// </summary>
            /// <param name="lineStart">Char index to begin drawing</param>
            public LineDisplaySubstring(string text, int lineStart)
            {
                this.text = text;
                StartIndex = lineStart;
                Length = 0;
            }

            /// <summary>
            ///   Creates a (lineStart, lineLength) pair.
            /// </summary>
            /// <param name="lineStart">Char index to begin drawing</param>
            /// <param name="lineLength">Number of chars to draw</param>
            public LineDisplaySubstring(string text, int lineStart, int lineLength)
            {
                this.text = text;
                StartIndex = lineStart;
                Length = lineLength;
            }

            /// <summary>
            ///   Number of chars to draw
            /// </summary>
            public int Length
            {
                get { return this.length; }
                set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException(String.Format("Line length must be nonnegative: {0}", value));
                    if (StartIndex + value > text.Length)
                        throw new ArgumentOutOfRangeException(String.Format("Line length must not extend outside text: {0}", value));
                    this.length = value;
                }
            }

            /// <summary>
            ///   Char index to begin drawing the line
            /// </summary>
            public int StartIndex
            {
                get { return this.startIndex; }
                set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("Line start must be nonnegative");
                    if (value > this.text.Length)
                        throw new ArgumentOutOfRangeException("Line start must be inside text");
                    this.startIndex = value;
                }
            }

            /// <summary>
            ///   Returns the computed substring for the line to display.
            /// </summary>
            /// <returns>
            ///   The line of text to display.
            /// </returns>
            public override string ToString()
            {
                return this.text.Substring(this.StartIndex, this.Length);
            }
        }
    }
}
