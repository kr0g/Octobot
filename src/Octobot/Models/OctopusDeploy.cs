namespace Octobot.Models
{
    public class OctopusDeploy
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public Project Project { get; set; }
    }
}