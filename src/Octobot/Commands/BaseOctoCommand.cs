using Octobot.Models;
using Octobot.Services;

namespace Octobot.Commands
{
    public abstract class BaseProjectCommand : IProjectCommand
    {
        protected IOctopusService Service { get; private set; }

        protected BaseProjectCommand(IOctopusService service)
        {
            Service = service;
        }

        public abstract Project Run(Project item);
        public abstract int Precidence { get; }
    }
}