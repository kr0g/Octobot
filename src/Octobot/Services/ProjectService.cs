using System.Collections.Generic;
using Octobot.Extensions;
using Octobot.Models;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octobot.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectResource project;
        private readonly IOctopusRepository repository;

        public ProjectService(ProjectResource project, IOctopusRepository repository)
        {
            this.project = project;
            this.repository = repository;
        }

        public VariableSetResource Variables => repository.VariableSets.Get(project.VariableSetId);

        public void ApplyVariables(IEnumerable<Variable> itemVariables)
        {
            var currentVariables = Variables;
            itemVariables.ForEach(each => ApplyVariable(each, currentVariables));
            repository.VariableSets.Modify(currentVariables);
        }

        private void ApplyVariable(Variable variable, VariableSetResource projectVariables)
        {
            var environment = GetEnvironmentValueFor(variable);
            var specification = new ScopeSpecification {{ScopeField.Environment, GetEnvironmentValueFor(variable)}};
            projectVariables.AddOrUpdateVariableValue(variable.Name, variable.Value, specification);
        }

        private ScopeValue GetEnvironmentValueFor(Variable variable)
        {
            if (variable.Environment.IsEmpty()) return new ScopeValue{};
            var env = repository.Environments.FindByName(variable.Environment);
            return new ScopeValue(env.Id);
        }
    }
}