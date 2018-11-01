using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public class CalculatorService : ServiceBase<CalculatorService>, ICalculatorService
    {
        private readonly ICalculationEngine calculationEngine;
        public CalculatorService(ICalculationEngine calculationEngine, ILogger<CalculatorService> logger) : base(logger)
        {
            this.calculationEngine = calculationEngine;
        }
        public int Calculate(int a, int b, string operation)
        {
            int result;
            switch (operation)
            {
                case "+":
                    result = calculationEngine.Add(a, b);
                    break;
                case "-":
                    result = calculationEngine.Substract(a, b);
                    break;
                case "*":
                    result = calculationEngine.Multiply(a, b);
                    break;
                case "/":
                    result = calculationEngine.Divide(a, b);
                    break;
                default:
                    string msg = $"Not supported {operation}";
                    logger.LogError(msg);
                    throw new ArgumentOutOfRangeException(
                        nameof(operation), msg);
            }

            return result;

        }
    }
}
