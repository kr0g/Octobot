using FluentValidation;
using Octobot.Extensions;

namespace Octobot.Validators
{
    public interface IValidateOctobotArguments : IValidator<string[]>
    {
        
    }
    public class ValidateOctobotArguments : AbstractValidator<string[]>, IValidateOctobotArguments
    {
        public ValidateOctobotArguments()
        {
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("You must provide a project file argument");
            RuleFor(x => x).ArgumentPropertyFileExists(0);
        }
    }
}