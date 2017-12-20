using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tasks
{
    public class TaskList
    {
        private List<Task> taskList = new List<Task>();
        private object myLock = new object();

        public TaskList()
        {
        }

        public void Add(Task task)
        {
            lock (myLock)
            {
                if (!taskList.Contains(task))
                    taskList.Add(task);
            }
        }

        public void Remove(Task task)
        {
            lock (myLock)
            {
                if (taskList.Contains(task))
                    taskList.Remove(task);
            }
        }

        // TODO: Implement something for iterating through the members of the list

    }
}
