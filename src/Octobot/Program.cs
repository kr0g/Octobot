using Common.Container;
using Octobot.Extensions;
using Octobot.Tasks;
using Octobot.Validators;

namespace Octobot
{
    public class Program
    {
        private static void Main(string[] args)
        {
            new StructureMapSetupTask().Run();

            IOC.Resolve<IValidateOctobotArguments>().Validate(args).ThrowResults();

            var project = IOC.Resolve<ParseProjectTask>().Run(args[0]);
            var commands = IOC.Resolve<OctobotCommandChain>();
            commands.Run(project);
        }
    }
}