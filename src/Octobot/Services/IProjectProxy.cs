using System.Collections.Generic;
using Octobot.Models;
using Octopus.Client.Model;

namespace Octobot.Services
{
    public interface IProjectProxy
    {
        void ApplyVariables(IEnumerable<Variable> itemVariables);
    }
}