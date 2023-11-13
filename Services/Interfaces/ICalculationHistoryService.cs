using astra_calc.Models;

namespace astra_calc.Services.Interfaces
{
    public interface ICalculationHistoryService
    {
        void SaveCalculation(string expression, string result);
        List<CalculationRecord> GetLastCalculations(int count);
    }
}
