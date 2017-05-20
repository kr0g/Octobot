using System;
using Octobot.Models;
using Octobot.Services;
using Octopus.Client;

namespace Octobot.Commands
{
    public class CreateProjectCommand : IOctoCommand
    {
        private readonly IOctopusProxyFactory proxyFactory;

        public CreateProjectCommand(IOctopusProxyFactory proxyFactory)
        {
            this.proxyFactory = proxyFactory;
        }

        public int Precidence => 0;
        public Project Run(Project item)
        {
            var octo = new OctopusServerEndpoint(item.OctopusUrl, item.ApiKey);
            var project = proxyFactory.GetProjectProxy(octo, item.Name);
            if (project == null)
                throw new ApplicationException($"{item.Name} project does not exist");
            project.ApplyVariables(item.Variables);
            return item;
        }

    }
}