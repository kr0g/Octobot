using Octobot.Models;
using Octobot.Services;

namespace Octobot.Commands
{
    public class ApplyVariablesCommand : BaseProjectCommand
    {
        public ApplyVariablesCommand(IOctopusService service) : base(service)
        {
        }
        public override int Precidence => 1;

        public override Project Run(Project item)
        {
            Service.Project.ApplyVariables(item.Variables);
            return item;
        }
    }
}