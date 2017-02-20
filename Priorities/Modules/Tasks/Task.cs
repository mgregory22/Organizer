//
// Priorities/Modules/Tasks/Task.cs
//

using System;

namespace Priorities.Modules.Tasks
{
    public class Task : IEquatable<Task>
    {
        string name;

        public Task(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name == null)
                    throw new ArgumentNullException("Task name cannot be null");
                if (name == "")
                    throw new ArgumentException("Task name cannot be empty");
                name = value;
            }
        }

        public bool Equals(Task other)
        {
            return name.Equals(other.name);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }

}
