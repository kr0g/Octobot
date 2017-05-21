namespace Octobot.Models
{
    public class Variable
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string[] Environments { get; set; }
        public bool IsSensitive { get; set; }
    }
}