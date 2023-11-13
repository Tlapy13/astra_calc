using astra_calc.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Serilog;

namespace astra_calc.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _calculatorService;
        private readonly ICalculationHistoryService _historyService;
        private readonly IErrorHandlingService _errorHandlingService;

        public CalculatorController(ICalculatorService calculatorService, 
                                    ICalculationHistoryService historyRepository,
                                     IErrorHandlingService errorHandlingService)
        {
            _calculatorService = calculatorService;
            _historyService = historyRepository;
            _errorHandlingService = errorHandlingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate([FromBody] CalculationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Expression))
            {
                return Json(new { error = "Expression is empty or invalid." });
            }
            try
            {
                var result = _calculatorService.EvaluateExpression(request.Expression);

                _historyService.SaveCalculation(request.Expression, result);

                return Json(new { result = result });
            }
            catch (Exception ex)
            {
                _errorHandlingService.SendError(ex, "CalculatorController - Error occurred in Calculate!");
                return Json(new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult History()
        {
            try
            {
                var history = _historyService.GetLastCalculations(10);
                return Json(history);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SendError(ex, "CalculatorController - Error occurred while getting history!");
                return Json(new { error = ex.Message });
            }
        }
    }
}

