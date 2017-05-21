using Common.Commands;
using Octobot.Models;

namespace Octobot.Commands
{
    public interface IOctpusCommand : ICommand<OctopusDeploy>
    {
        int Precidence { get; }
    }
}
