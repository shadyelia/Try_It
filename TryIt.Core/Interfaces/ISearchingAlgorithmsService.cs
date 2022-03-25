using TryIt.Core.Enums;

namespace TryIt.Core.Interfaces
{
    public interface ISearchingAlgorithmsService
    {
        string Search(SearchAlgorithmTypesEnum type);
    }
}
