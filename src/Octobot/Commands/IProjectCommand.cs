using Common.Commands;
using Octobot.Models;

namespace Octobot.Commands
{
    public interface IProjectCommand : ICommand<Project>
    {
        int Precidence { get; }
    }
}
