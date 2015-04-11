using MSG.IO;
using System.Collections.Generic;

namespace MSGTest.IO
{
    public class TestRead : Read
    {
        char? nextKey;
        List<char> nextKeys;
        string nextString;
        List<string> nextStrings;
        public override char Key()
        {
            if (nextKey != null)
            {
                char c = nextKey.GetValueOrDefault();
                nextKey = null;
                return c;
            }
            else if (nextKeys != null && nextKeys.Count > 0)
            {
                char c = nextKeys[0];
                nextKeys.RemoveAt(0);
                return c;
            }
            return ' ';
        }
        public char NextKey
        {
            get { return nextKey.GetValueOrDefault(); }
            set { nextKey = value; }
        }
        public char[] NextKeys
        {
            get { return nextKeys.ToArray(); }
            set { nextKeys = new List<char>(value); }
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
    }
}
