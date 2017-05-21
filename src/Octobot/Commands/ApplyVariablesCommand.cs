using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Common;
using Octobot.Extensions;
using Octobot.Models;
using Octobot.Services;
using Octopus.Client.Model;

namespace Octobot.Commands
{
    public class ApplyVariablesCommand : BaseOctpusCommand
    {
        public ApplyVariablesCommand(IOctopusService service) : base(service)
        {
        }

        public override int Precidence => 2;

        public override OctopusDeploy Run(OctopusDeploy item)
        {
            item.Project.Variables.ForEach(each =>
            {
                var scopeSpecification = CreateScopeSpecification(each);
                Service.Variable.AddOrUpdateVariable(each.Name, each.Value, scopeSpecification);
            });
            return item;
        }
        
        private ScopeSpecification CreateScopeSpecification(Variable var)
        {
            var environments = GetEnvironments(var.Environments);
            return new ScopeSpecification {{ScopeField.Environment, GetIdsFrom(environments)}};
        }

        private IEnumerable<EnvironmentResource> GetEnvironments(string[] environments)
        {
            if (environments.IsEmpty()) return Enumerable.Empty<EnvironmentResource>();
            return environments.Select(each => Service.RepositoryService.FindEnvironmentByName(each))
                               .Where(x => !ReferenceEquals(null, x)).ToList();
        }

        private static ScopeValue GetIdsFrom(IEnumerable<EnvironmentResource> environments)
        {
            if (environments.IsEmpty()) return new ScopeValue();
            return new ScopeValue(environments.Select(x => x.Id));
        }
    }
}