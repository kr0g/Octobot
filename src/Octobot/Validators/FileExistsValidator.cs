using FluentValidation;
using Octobot.Extensions;

namespace Octobot.Validators
{
    public interface IFileSystemValidator : IValidator<string>
    {
    }

    public class FileSystemValidator : AbstractValidator<string>, IFileSystemValidator 
    {
        public FileSystemValidator()
        {
            RuleFor(x => x).FileExists();
        }
    }
}
