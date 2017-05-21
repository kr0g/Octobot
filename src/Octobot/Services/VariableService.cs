using Octopus.Client.Model;

namespace Octobot.Services
{
    public class VariableService : IVariableService
    {
        private readonly ProjectResource project;
        private readonly IRepositoryService repository;
        private readonly VariableSetResource variables;

        public VariableService(ProjectResource project, IRepositoryService repository)
        {
            this.project = project;
            this.repository = repository;
            variables = repository.VariableSets.Get(project.VariableSetId);
        }
        
        public void AddOrUpdateVariable(string name, string value, ScopeSpecification specification)
        {
            variables.AddOrUpdateVariableValue(name, value, specification);
        }

        public void Save()
        {
            repository.VariableSets.Modify(variables);
        }
    }
}