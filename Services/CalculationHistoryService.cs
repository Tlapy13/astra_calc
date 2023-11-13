using astra_calc.Models;
using astra_calc.Services.Interfaces;
using Database;
using Database.Interfaces;

namespace astra_calc.Services
{
    public class CalculationHistoryService: ICalculationHistoryService
    {
        private readonly ICalculationDataAccess _CalcDataAccess;

        public CalculationHistoryService(ICalculationDataAccess CalcDataAccess)
        {
            _CalcDataAccess = CalcDataAccess;
        }

        public void SaveCalculation(string expression, string result)
        {
            _CalcDataAccess.SaveCalculation(expression, result);
        }

        public List<CalculationRecord> GetLastCalculations(int count)
        {
            return _CalcDataAccess.GetLastCalculations(count);
        }
    }

    }
