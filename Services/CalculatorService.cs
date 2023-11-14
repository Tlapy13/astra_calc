using astra_calc.Services.Interfaces;
using Calculator_Logic;
using Calculator_Logic.Interfaces;

namespace astra_calc.Services
{
    public class CalculatorService: ICalculatorService
    {
        private readonly ICalculate _calculate;

        public CalculatorService(ICalculate calculate)
        {
            _calculate = calculate;
        }

        public string EvaluateExpression(string expression)
        {
            return _calculate.EvaluateExpression(expression);
        }
    }
}
