using PupaParserComeback.Presentation.RecruitCommands.Parameters;
using System.Threading.Tasks;

namespace PupaParserComeback.Presentation.Abstract
{
    public interface IParameterizedCommandAsync<T>
    {
        Task ExecuteAsync(T parameters);
    }
}
