using System;
namespace InitMiddlewaresDI.Code
{
    public interface IAdder
    {
        string Add(int a, int b);
    }

    public class BasicCalculator : IAdder
    {
        public string Add(int a, int b) => $"{a}+{b}={a + b}";
    }
}
