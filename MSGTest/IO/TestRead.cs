using MSG.IO;
using System.Collections.Generic;

namespace MSGTest.IO
{
    public class TestRead : Read
    {
        char? nextChar;
        List<char> nextChars;
        string nextString;
        List<string> nextStrings;

        public override char Char()
        {
            if (nextChar != null)
            {
                char c = nextChar.GetValueOrDefault();
                nextChar = null;
                return c;
            }
            else if (nextChars != null && nextChars.Count > 0)
            {
                char c = nextChars[0];
                nextChars.RemoveAt(0);
                return c;
            }
            return ' ';
        }

        public char NextKey
        {
            get { return nextChar.GetValueOrDefault(); }
            set { nextChar = value; }
        }

        public char[] NextKeys
        {
            get { return nextChars.ToArray(); }
            set { nextChars = new List<char>(value); }
        }

        public override string String()
        {
            if (nextString != null)
            {
                string s = nextString;
                nextString = null;
                return s;
            }
            else if (nextStrings != null && nextStrings.Count > 0)
            {
                string s = nextStrings[0];
                nextStrings.RemoveAt(0);
                return s;
            }
            return "";
        }

        public string NextString
        {
            get { return nextString; }
            set { nextString = value; }
        }

        public string[] NextStrings
        {
            get { return nextStrings.ToArray(); }
            set { nextStrings = new List<string>(value); }
        }

        public TestRead(Print print)
            : base(print)
        {
        }
    }
}
