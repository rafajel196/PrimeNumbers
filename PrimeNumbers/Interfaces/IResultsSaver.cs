using PrimeNumbers.DTO;

namespace PrimeNumbers.Interfaces
{
    internal interface IResultsSaver
    {
        Task SaveResultsToFileAsync(string filePath, List<CycleResultDto> results);
    }
}