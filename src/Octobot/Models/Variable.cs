namespace Octobot.Models
{
    public class Variable
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Scope { get; set; }
        public string Environment { get; set; }
        public bool IsSensitive { get; set; }
    }
}