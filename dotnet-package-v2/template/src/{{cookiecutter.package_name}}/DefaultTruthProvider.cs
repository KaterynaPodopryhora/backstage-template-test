using Funda.Extensions.ArgumentValidation;
using {{cookiecutter.package_name}};
using System.Threading.Tasks;

namespace {{cookiecutter.package_name}}
{
    ///<inheritdoc/>
    public class DefaultTruthProvider : ITruthProvider
    {
        ///<inheritdoc/>
        public Task<int> GetTruth(object argument)
        {
            ArgumentValidator.For(nameof(argument), argument).NotNull();

            return Task.FromResult(42);
        }
    }
}
