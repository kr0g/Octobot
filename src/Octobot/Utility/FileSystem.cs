using System.IO;
using Jil;
using Octobot.Extensions;
using Octobot.Validators;

namespace Octobot.Utility
{
    public class FileSystem : IFileSystem
    {
        private readonly IFileSystemValidator fileSystemValidator;

        public FileSystem(IFileSystemValidator fileSystemValidator)
        {
            this.fileSystemValidator = fileSystemValidator;
        }
        public T ReadJsonFile<T>(string file) where T : class, new()
        {
            fileSystemValidator.Validate(file).ThrowResults();
            return JSON.Deserialize<T>(File.ReadAllText(file));
        }
    }
}