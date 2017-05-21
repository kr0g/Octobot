using Octobot.Models;
using Octobot.Services;

namespace Octobot.Commands
{
    public class CreateEnvironmentsCommand : BaseOctpusCommand
    {
        public CreateEnvironmentsCommand(IOctopusService service) : base(service)
        {
        }

        public override int Precidence => 1;

        public override OctopusDeploy Run(OctopusDeploy item)
        {
            return item;
        }

    }
}
