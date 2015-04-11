﻿using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class MoveTask : TaskCommand
    {
        public MoveTask(Print print, Read read, Tasks tasks)
            : base(print, read, tasks)
        {
        }
    }
}