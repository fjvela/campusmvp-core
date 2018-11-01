using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        private ICalculationEngine calculationEngine;
        public CalculatorService(ICalculationEngine calculationEngine)
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

                    throw new ArgumentOutOfRangeException(
                        nameof(operation),
                        $"Not supported {operation}");
            }

            return result;

        }
    }
}
