using Octopus.Client.Model;

namespace Octobot.Services
{
    public class VariableService : IVariableService
    {
        private readonly IRepositoryService repository;
        private readonly VariableSetResource variables;

        public VariableService(ProjectResource project, IRepositoryService repository)
        {
            this.repository = repository;
            variables = repository.VariableSets.Get(project.VariableSetId);
        }
        
        public void AddOrUpdateVariable(string name, string value, ScopeSpecification specification, bool isSensitive=false)
        {
            variables.AddOrUpdateVariableValue(name, value, specification, isSensitive);
        }

        public void Save()
        {
            repository.VariableSets.Modify(variables);
        }
        
    }
}