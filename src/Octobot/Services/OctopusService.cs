using Octobot.Models;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octobot.Services
{
    public class OctopusService : IOctopusService
    {
        public IRepositoryService RepositoryService { get; set; }
        public IVariableService Variable { get; private set; }

        public IVariableService FindOrCreateProject(Project model)
        {
            var project = RepositoryService.Projects.FindByName(model.Name) ?? CreateProject(model);
            return Variable = new VariableService(project, RepositoryService);
        }

        public ProjectResource CreateProject(Project model)
        {
            RepositoryService.Projects.CreateOrModify(model.Name, RepositoryService.DefaultProjectGroup,
                                                      RepositoryService.DefaultLifecycle, model.Description).Save();
            return RepositoryService.Projects.FindByName(model.Name); 
        }

        public void Initialize(OctopusServerEndpoint endpoint)
        {
            RepositoryService = new RepositoryService(new OctopusRepository(endpoint));
        }
    }
}