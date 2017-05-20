using Octobot.Commands;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octobot.Services
{
    public interface IOctopusProxyFactory
    {
        IProjectProxy GetProjectProxy(OctopusServerEndpoint endpoint, string name);
    }

    public class OctopusProxyFactory : IOctopusProxyFactory
    {
        public IProjectProxy GetProjectProxy(OctopusServerEndpoint endpoint, string name)
        {
            var repository = new OctopusRepository(endpoint);
            var project = repository.Projects.FindByName(name) ?? CreateProject(repository, name);
            return new ProjectProxy(project, repository);
        }

        private ProjectResource CreateProject(IOctopusRepository repository, string projectName)
        {
            var lifecycle = repository.Lifecycles.FindAll()[0];
            var resource = repository.ProjectGroups.FindAll()[0];
            var project = repository.Projects.CreateOrModify(projectName, resource, lifecycle);
            project.Save();
            return repository.Projects.FindByName(projectName);
        }
    }
}
