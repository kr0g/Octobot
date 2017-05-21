using Octopus.Client;
using Octopus.Client.Model;

namespace Octobot.Services
{
    public interface IOctopusService
    {
        ProjectResource FindOrCreateProject(IOctopusRepository repository, string projectName);
        IProjectService GetProjectProxy(OctopusServerEndpoint endpoint, string name);
        IOctopusRepository Repository { get; }
        IProjectService Project { get; }
    }
}
