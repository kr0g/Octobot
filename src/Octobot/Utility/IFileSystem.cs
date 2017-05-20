namespace Octobot.Utility
{
    public interface IFileSystem
    {
        T ReadJsonFile<T>(string file) where T : class, new();
    }
}
