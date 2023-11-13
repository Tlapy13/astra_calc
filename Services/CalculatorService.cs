using astra_calc.Services.Interfaces;
using Calculator_Logic;

namespace astra_calc.Services
{
    public class CalculatorService: ICalculatorService
    {
        private readonly Calculate _calculate;

        public CalculatorService()
        {
            _calculate = new Calculate();
        }

        public string EvaluateExpression(string expression)
        {
            return _calculate.EvaluateExpression(expression);
        }
    }
}
