using System.Collections.Generic;
using System.Linq;
using Octobot.Extensions;
using Octobot.Models;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octobot.Services
{
    public class ProjectProxy : IProjectProxy
    {
        private readonly ProjectResource project;
        private readonly OctopusRepository repository;

        public ProjectProxy(ProjectResource project, OctopusRepository repository)
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

        private static void ApplyVariable(Variable variable, VariableSetResource projectVariables)
        {
            var specification = new ScopeSpecification {{ScopeField.Environment, variable.Environment}};
            projectVariables.AddOrUpdateVariableValue(variable.Name, variable.Value, specification);
        }
    }
}