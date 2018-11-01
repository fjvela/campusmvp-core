﻿using System;

namespace Calculator.Services
{
    public class CalculationEngine : ICalculationEngine
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Divide(int a, int b)
        {
            if (b == 0)
                throw new InvalidOperationException($"Can't divide by {b}");

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