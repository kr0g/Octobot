using Octopus.Client.Model;

namespace Octobot.Services
{
    public interface IVariableService
    {
        void AddOrUpdateVariable(string varName, string varValue, ScopeSpecification scopeSpecification);
        void Save();
    }
}