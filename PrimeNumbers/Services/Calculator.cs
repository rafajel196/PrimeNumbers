using PrimeNumbers.DTO;
using PrimeNumbers.Interfaces;

internal sealed class Calculator : ICalculator
{
    private ulong _lastPrime = 2;
    private int _cycleCount = 0;
    private readonly CancellationToken _cancellationToken;
    private readonly List<CycleResultDto> _results = new List<CycleResultDto>();

    public bool IsCycleRunning { get; private set; }

    public event Action<string> ErrorOccurred;
    public event Action<CycleResultDto> CycleCompleted;

    public Calculator(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
    }

    public async Task StartCalculatingAsync()
    {
        try
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                _cycleCount++;
                var cycleStartTime = DateTime.Now;
                var cycleDuration = TimeSpan.FromMinutes(2);
                var endTime = cycleStartTime.Add(cycleDuration);

                IsCycleRunning = true;

                await Task.Run(() =>
                {
                    while (DateTime.Now < endTime && !_cancellationToken.IsCancellationRequested)
                    {
                        var nextPrime = GetNextPrime(_lastPrime);
                        if (nextPrime == null)
                        {
                            ErrorOccurred?.Invoke("Nie udało się ustalić kolejnej liczby pierwszej.");
                            IsCycleRunning = false;
                            return;
                        }
                        _lastPrime = nextPrime.Value;
                    }
                });

                IsCycleRunning = false;

                var cycleResult = new CycleResultDto
                {
                    CycleNumber = _cycleCount,
                    CycleDuration = DateTime.Now - cycleStartTime,
                    ResultTime = DateTime.Now,
                    LargestPrime = _lastPrime
                };

                _results.Add(cycleResult);
                CycleCompleted?.Invoke(cycleResult);

                if (!_cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(60000);
                }
            }

            IsCycleRunning = false;
        }
        catch (Exception ex)
        {
            IsCycleRunning = false;
            ErrorOccurred?.Invoke($"Wystąpił błąd: {ex.Message}");
        }
    }

    private ulong? GetNextPrime(ulong start)
    {
        var number = start + 1;

        while (true)
        {
            if (IsPrime(number))
                return number;

            number++;
            if (number == ulong.MaxValue)
                return null;
        }
    }

    private bool IsPrime(ulong number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        for (ulong i = 3; i <= (ulong)Math.Sqrt(number); i += 2)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }

}