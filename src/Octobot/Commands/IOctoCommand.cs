using Common.Commands;
using Octobot.Models;

namespace Octobot.Commands
{
    public interface IOctoCommand : ICommand<Project>
    {
        int Precidence { get; }
    }
}
