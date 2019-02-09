using System;
using System.Diagnostics;

namespace Com.SampleLibrary
{
    public class FibonacciGeneratorUsingDP : IFibonacciGenerator
    {
        private long[] TempStore;

        public long Generate(int n)
        {
            TempStore = new long[n + 1];
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i <= n; i++)
            {
                TempStore[i] = -1;
            }
            var result = this.Fibonacci(n);
            watch.Stop();
            Console.WriteLine($"ExecutionTime : {watch.Elapsed}");
            Console.WriteLine($"Value is : {result}");
            return result;
        }
        private long Fibonacci(int n)
        {
            if (TempStore[n] != -1)
            {
                return TempStore[n];
            }
            if (n == 0 || n == 1)
            {
                TempStore[n] = 1;
                return 1;
            }
            if (n == 2)
            {
                TempStore[n] = 2;
                return 2;
            }

            TempStore[n] = Fibonacci(n - 1) + Fibonacci(n - 2);
            return TempStore[n];
        }
    }
}
