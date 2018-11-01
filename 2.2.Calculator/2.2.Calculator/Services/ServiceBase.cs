using Microsoft.Extensions.Logging;

namespace Calculator.Services
{
    public abstract class ServiceBase<T> where T : class
    {
        protected readonly ILogger<T> logger;

        protected ServiceBase(ILogger<T> logger)
        {
            this.logger = logger;
        }
    }
}