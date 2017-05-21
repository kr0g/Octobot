using Octopus.Client;
using Octopus.Client.Model;

namespace Octobot.Services
{
    public class OctopusService : IOctopusService
    {
        public IOctopusRepository Repository { get; private set; }
        public IProjectService Project { get; private set; }

        public IProjectService GetProjectProxy(OctopusServerEndpoint endpoint, string name)
        {
            Repository = new OctopusRepository(endpoint);
            var project = Repository.Projects.FindByName(name) ?? FindOrCreateProject(Repository, name);
            Project = new ProjectService(project, Repository);
            return Project;
        }

        public ProjectResource FindOrCreateProject(IOctopusRepository repository, string projectName)
        {
            var lifecycle = repository.Lifecycles.FindAll()[0];
            var resource = repository.ProjectGroups.FindAll()[0];
            repository.Projects.CreateOrModify(projectName, resource, lifecycle).Save();
            return repository.Projects.FindByName(projectName); 
        }

    }
}