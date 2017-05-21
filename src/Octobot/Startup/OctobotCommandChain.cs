using System.Collections.Generic;
using System.Linq;
using Common.Commands;
using Octobot.Commands;
using Octobot.Extensions;
using Octobot.Models;

namespace Octobot.Startup
{
    public class OctobotCommandChain : ICommand<OctopusDeploy>
    {
        private readonly IEnumerable<IOctpusCommand> commands;

        public OctobotCommandChain(IEnumerable<IOctpusCommand> commands)
        {
            this.commands = commands;
        }

        public OctopusDeploy Run(OctopusDeploy item)
        {
            commands.OrderBy(x => x.Precidence).ForEach(each => each.Run(item));
            return item;
        }
    }
}
