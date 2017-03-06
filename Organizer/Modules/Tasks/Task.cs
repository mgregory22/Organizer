//
// Organizer/Modules/Tasks/Task.cs
//

using System;
using MSG.Types.Dir;

namespace Organizer.Modules.Tasks
{
    public class Task
    {
        DateTime time;

        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime Time
        {
            get { return time; }
            set
            {
                // Not sure which one to do:
                // Let the db set the current time if value == null
                time = value;
                // Set the current time if value == null
                // time = (value == null) ? DateTime.UtcNow : value;
            }
        }
    }

}
