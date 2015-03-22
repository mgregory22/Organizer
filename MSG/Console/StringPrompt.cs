using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    public class StringPrompt : Prompt
    {
        public StringPrompt(Print print, string promptMsg, Read read)
            : base(print, promptMsg, read)
        {

        }
    }
}
