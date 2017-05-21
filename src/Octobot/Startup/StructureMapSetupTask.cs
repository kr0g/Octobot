using Common.Commands;
using Common.Container;
using Common.Tasks;
using FluentValidation;
using Octobot.Services;
using Octobot.Utility;
using Octobot.Validators;
using StructureMap;

namespace Octobot.Startup
{
    public class StructureMapSetupTask : ITask
    {
        public void Run()
        {
            var container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.AssemblyContainingType<Program>();
                    x.WithDefaultConventions();
                    x.RegisterConcreteTypesAgainstTheFirstInterface();
                    x.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                    x.ConnectImplementationsToTypesClosing(typeof(ICommand<>));
                });
                _.For<IOctopusService>().Use<OctopusService>().Singleton();
                _.For<IValidateOctobotArguments>().Use<ValidateOctobotArguments>();
                _.For<IFileSystemValidator>().Use<FileSystemValidator>();
                _.For<IFileSystem>().Use<FileSystem>();
            });

            IOC.Register(new StructureMapDependencyResolver(container));
        }
    }
}