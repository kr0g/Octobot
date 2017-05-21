using Octopus.Client;
using Octopus.Client.Model;
using Octopus.Client.Repositories;

namespace Octobot.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly IOctopusRepository repository;

        public RepositoryService(IOctopusRepository repository)
        {
            this.repository = repository;
        }

        public IVariableSetRepository VariableSets => repository.VariableSets;
        public IProjectRepository Projects => repository.Projects;
        public LifecycleResource DefaultLifecycle => repository.Lifecycles.FindAll()[0];
        public ProjectGroupResource DefaultProjectGroup => repository.ProjectGroups.FindAll()[0];
        public IEnvironmentRepository Environments => repository.Environments;
        public EnvironmentResource FindEnvironmentByName(string name)
        {
            return repository.Environments.FindByName(name);
        }
    }
}
