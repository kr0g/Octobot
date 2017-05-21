using Octobot.Models;
using Octobot.Services;
using Octopus.Client;

namespace Octobot.Commands
{
    public class CreateOctpusCommand : BaseOctpusCommand
    {

        public CreateOctpusCommand(IOctopusService service) : base(service)
        {
        }
        public override int Precidence => 0;

        public override OctopusDeploy Run(OctopusDeploy item)
        {
            var endpoint = new OctopusServerEndpoint(item.Url, item.ApiKey);
            Service.Initialize(endpoint);
            Service.FindOrCreateProject(item.Project);
            return item;
        }

    }
}
