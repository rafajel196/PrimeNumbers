using PrimeNumbers.DTO;
using PrimeNumbers.Interfaces;
using System.Xml.Serialization;

namespace PrimeNumbers.Services
{
    internal sealed class ResultsSaver : IResultsSaver
    {
        public async Task SaveResultsToFileAsync(string filePath, List<CycleResultDto> results)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<CycleResultDto>));

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Task.Run(() => serializer.Serialize(fileStream, results));
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Błąd podczas zapisywania wyników: " + ex.Message);
            }
        }
    }
}