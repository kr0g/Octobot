using Octobot.Models;
using Octobot.Services;
using Octopus.Client;

namespace Octobot.Commands
{
    public class CreateProjectCommand : BaseProjectCommand
    {

        public CreateProjectCommand(IOctopusService service) : base(service)
        {
        }
        public override int Precidence => 0;

        public override Project Run(Project item)
        {
            var endpoint = new OctopusServerEndpoint(item.OctopusUrl, item.ApiKey);
            Service.GetProjectProxy(endpoint, item.Name);
            return item;
        }

    }
}
