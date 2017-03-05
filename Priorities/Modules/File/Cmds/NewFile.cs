﻿//
// Priorities/Modules/File/Cmds/NewFile.cs
//

using MSG.Patterns;

namespace Priorities.Modules.File.Cmds
{
    public class NewFile : Cmd
    {
        protected string fileName;

        public NewFile(string fileName)
        {
            this.fileName = fileName;
        }

        public override Result Do()
        {
            return CANTDO;
        }

        public override Result Undo()
        {
            return CANTUNDO;
        }
    }
}