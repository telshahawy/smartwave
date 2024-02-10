using Microsoft.Extensions.DependencyInjection;

namespace SW.Framework.DependencyInjection
{
    /// <summary>
    ///     Represents an application module. The module class is responsible for registering the correct service definitions
    ///     and their implementation in each application module.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        ///     Initialises the current module instance by passing specified container.
        /// </summary>
        /// <param name="container">The dependency injection container.</param>
        void Initialise(IServiceCollection services);
    }
}