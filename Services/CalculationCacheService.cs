using astra_calc.Models;
using astra_calc.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace astra_calc.Services
{
    public class CalculationCacheService : ICalculationHistoryService
    {
        private readonly IMemoryCache _cache;
        private const string HistoryKey = "CalculationHistory";

        public CalculationCacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void SaveCalculation(string expression, string result)
        {
            var history = _cache.GetOrCreate(HistoryKey, entry => new List<CalculationRecord>());
            history.Add(new CalculationRecord
            {
                Expression = expression,
                Result = result,
                Timestamp = DateTime.UtcNow
            });
            _cache.Set(HistoryKey, history);
        }

        public List<CalculationRecord> GetLastCalculations(int count)
        {
            var history = _cache.Get<List<CalculationRecord>>(HistoryKey) ?? new List<CalculationRecord>();
            return history.OrderByDescending(x => x.Timestamp).Take(count).ToList();
        }
    }
}
