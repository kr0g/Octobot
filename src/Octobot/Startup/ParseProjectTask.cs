using Octobot.Models;
using Octobot.Utility;

namespace Octobot.Startup
{
    public class ParseProjectTask
    {
        private readonly IFileSystem fileSystem;

        public ParseProjectTask(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public OctopusDeploy Run(string fileName)
        {
            return fileSystem.ReadJsonFile<OctopusDeploy>(fileName);
        }
    }
}
