using System.Collections.Generic;

namespace Octobot.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string OctopusUrl { get; set; }
        public string ApiKey { get; set; }
        public IEnumerable<Variable> Variables { get; set; }
    }
}
