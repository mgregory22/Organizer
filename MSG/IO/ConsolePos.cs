using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.IO
{
    public class ConsolePos
    {
        int left;
        int top;

        public ConsolePos(int left, int top)
        {
            Left = left;
            Top = top;
        }

        public ConsolePos(ConsolePos pos)
        {
            Left = pos.Left;
            Top = pos.Top;
        }

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", left, top);
        }
    }
}
