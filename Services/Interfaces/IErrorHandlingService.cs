namespace astra_calc.Services.Interfaces
{
    public interface IErrorHandlingService
    {
        void SendError(Exception exception, string customMessage);
    }
}
