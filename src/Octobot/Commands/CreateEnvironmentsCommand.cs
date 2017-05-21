using System.Collections.Generic;
using System.Linq;
using Octobot.Extensions;
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
            GetEnvironments(item.Project).ForEach(each =>
            {
                Service.RepositoryService.Environments.CreateOrModify(each);
            });
            
            return item;
        }

        private IEnumerable<string> GetEnvironments(Project project)
        {
            return project.Variables.SelectMany(x => x.Environments).ToList();
        }
    }
}
