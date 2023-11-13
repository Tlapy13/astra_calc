using astra_calc.Services.Interfaces;
using Serilog;

namespace astra_calc.Services
{
    public class ErrorHandlingService : IErrorHandlingService
    {
        public void SendError(Exception exception, string customMessage)
        {
            Log.Error(exception, customMessage);
        }
    }
}
