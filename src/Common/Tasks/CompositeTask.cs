using System.Collections.Generic;
using Common.Commands;

namespace Common.Tasks
{
    public interface ITask : ICommand
    {
    }

    public class CompositeTask : ITask
    {
        readonly ICollection<ITask> tasks = new List<ITask>();

        public CompositeTask(params ITask[] toAdd)
        {
            toAdd.Each(tasks.Add);
        }

        public void Run()
        {
            tasks.Each(x => x.Run());
        }
    }
}