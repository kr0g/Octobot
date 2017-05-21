using System.Collections.Generic;

namespace Octobot.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Variable> Variables { get; set; }
    }
}
