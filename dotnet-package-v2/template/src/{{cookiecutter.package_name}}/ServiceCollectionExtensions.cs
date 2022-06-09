using {{cookiecutter.package_name}};
using Microsoft.Extensions.DependencyInjection;

namespace {{cookiecutter.package_name}}
{
    /// <summary>
    /// Extends the funcionality of the IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the dependencies of the {{cookiecutter.package_name}} into the dependency injection framework.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The <see cref="IServiceCollection"/> for further configuration. </returns>
        public static IServiceCollection Add{{cookiecutter.package_name|replace('.', '')}}(this IServiceCollection services)
        {
            services.AddTransient<ITruthProvider, DefaultTruthProvider>();

            return services;
        }
    }
}
