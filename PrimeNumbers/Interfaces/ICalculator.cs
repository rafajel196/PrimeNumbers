using PrimeNumbers.DTO;

namespace PrimeNumbers.Interfaces
{
    internal interface ICalculator
    {
        event Action<string> ErrorOccurred;
        event Action<CycleResultDto> CycleCompleted;

        Task StartCalculatingAsync();
        bool IsCycleRunning { get; }
    }
}