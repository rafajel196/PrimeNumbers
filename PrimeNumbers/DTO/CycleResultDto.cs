namespace PrimeNumbers.DTO
{
    public sealed class CycleResultDto
    {
        public int CycleNumber { get; set; }
        public TimeSpan CycleDuration { get; set; }
        public DateTime ResultTime { get; set; }
        public ulong LargestPrime { get; set; }
    }
}