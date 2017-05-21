using Octobot.Models;
using Octopus.Client;

namespace Octobot.Services
{
    public interface IOctopusService
    {
        void Initialize(OctopusServerEndpoint endpoint);
        IVariableService FindOrCreateProject(Project project);
        IVariableService Variable { get; }
        IRepositoryService RepositoryService { get; }
    }
}
