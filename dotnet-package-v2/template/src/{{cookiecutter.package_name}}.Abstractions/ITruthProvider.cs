using System.Threading.Tasks;

namespace {{cookiecutter.package_name}}
{
    /// <summary>
    /// This service does something very smart.
    /// </summary>
    public interface ITruthProvider
    {
        /// <summary>
        /// Gets the ultimate truth.
        /// </summary>
        /// <param name="argument">Argument to pass.</param>
        /// <returns>An awaitable <see cref="Task{int}"/> containing the value indicating the truth.</returns>
        Task<int> GetTruth(object argument);
    }
}
