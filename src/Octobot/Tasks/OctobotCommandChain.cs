using System.Collections.Generic;
using System.Linq;
using Common.Commands;
using Octobot.Commands;
using Octobot.Extensions;
using Octobot.Models;

namespace Octobot.Tasks
{
    public class OctobotCommandChain : ICommand<Project>
    {
        private readonly IEnumerable<IOctoCommand> commands;

        public OctobotCommandChain(IEnumerable<IOctoCommand> commands)
        {
            this.commands = commands;
        }

        public Project Run(Project item)
        {
            commands.OrderBy(x => x.Precidence).ForEach(each => each.Run(item));
            return item;
        }
        
    }
}
