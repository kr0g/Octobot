using System.Collections.Generic;

namespace Common.Commands
{
    public class CompositeCommand<T> : ICommand<T>
    {
        readonly ICollection<ICommand<T>> commands = new List<ICommand<T>>();

        public CompositeCommand(params ICommand<T>[] cmds )
        {
            cmds.Each(commands.Add);
        }

        public T Run(T subject)
        {
            commands.Each(x => x.Run(subject));
            return subject;
        }
    }
}