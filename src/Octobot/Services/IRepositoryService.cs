using Octopus.Client.Model;
using Octopus.Client.Repositories;

namespace Octobot.Services
{
    public interface IRepositoryService
    {
        IProjectRepository Projects { get; }
        LifecycleResource DefaultLifecycle { get; }
        ProjectGroupResource DefaultProjectGroup { get; }
        IVariableSetRepository VariableSets { get; }
        IEnvironmentRepository Environments { get; }
        EnvironmentResource FindEnvironmentByName(string name);
    }    
}