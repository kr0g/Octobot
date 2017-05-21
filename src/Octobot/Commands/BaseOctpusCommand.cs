using Octobot.Models;
using Octobot.Services;

namespace Octobot.Commands
{
    public abstract class BaseOctpusCommand : IOctpusCommand
    {
        protected IOctopusService Service { get; private set; }

        protected BaseOctpusCommand(IOctopusService service)
        {
            Service = service;
        }

        public abstract OctopusDeploy Run(OctopusDeploy item);
        public abstract int Precidence { get; }
    }
}