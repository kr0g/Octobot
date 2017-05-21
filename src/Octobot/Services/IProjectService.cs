using System.Collections.Generic;
using Octobot.Models;

namespace Octobot.Services
{
    public interface IProjectService
    {
        void ApplyVariables(IEnumerable<Variable> itemVariables);
    }
}