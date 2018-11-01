using Microsoft.Extensions.Logging;
using System;

namespace Calculator.Services
{
    public class CalculationEngine : ServiceBase<CalculationEngine>, ICalculationEngine
    {
        public CalculationEngine(ILogger<CalculationEngine> logger) : base(logger)
        {
            
        }
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Divide(int a, int b)
        {
            if (b == 0)
            {
                string msg = $"Can't divide by {b}";
                logger.LogWarning(msg);
                throw new InvalidOperationException(msg);
            }

            return a / b;
        }

        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public int Substract(int a, int b)
        {
            return a - b;
        }
    }
}