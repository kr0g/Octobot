using Common.Commands;
using Common.Container;
using Common.Tasks;
using FluentValidation;
using Octobot.Commands;
using Octobot.Models;
using Octobot.Services;
using Octobot.Utility;
using Octobot.Validators;
using StructureMap;
using StructureMap.Pipeline;

namespace Octobot.Tasks
{
    public class StructureMapSetupTask : ITask
    {
        public void Run()
        {
            var container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
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