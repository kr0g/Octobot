using Octobot.Models;
using Octobot.Utility;

namespace Octobot.Tasks
{
    public class ParseProjectTask
    {
        private readonly IFileSystem fileSystem;

        public ParseProjectTask(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Project Run(string fileName)
        {
            return fileSystem.ReadJsonFile<Project>(fileName);
        }
    }
}
