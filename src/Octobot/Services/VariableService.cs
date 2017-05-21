using Octopus.Client.Model;

namespace Octobot.Services
{
    public class VariableService : IVariableService
    {
        private readonly ProjectResource project;
        private readonly IRepositoryService repository;

        public VariableService(ProjectResource project, IRepositoryService repository)
        {
            this.project = project;
            this.repository = repository;
        }

        public VariableSetResource Variables => repository.VariableSets.Get(project.VariableSetId);
        
        public void AddOrUpdateVariable(string name, string value, ScopeSpecification specification)
        {
            Variables.AddOrUpdateVariableValue(name, value, specification);
        }

        public void Save()
        {
            repository.VariableSets.Modify(Variables);
        }
    }
}