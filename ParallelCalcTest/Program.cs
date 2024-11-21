using System.Diagnostics;

namespace ParallelCalcTest
{
    internal class Program
    {
        static long SerialCalc(long max)
        {
            long sum = 0;
            for (long i = 1; i <= max; i++)
            {
                sum += i;
            }
            return sum;
        }

        static void Main(string[] args)
        {
            long max = (long)1e9;
            int threadCount = 60000;

            ParallelCalc calc = new ParallelCalc(threadCount, max);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            long result = calc.StartCalc();

            stopwatch.Stop();
            Console.WriteLine("Result: {0}", result);
            Console.WriteLine("Elapsed time: {0} ms", stopwatch.ElapsedMilliseconds);

            // Serial calculation
            stopwatch.Restart();
            long serialResult = SerialCalc(max);
            stopwatch.Stop();
            Console.WriteLine("Serial result: {0}", serialResult);
            Console.WriteLine("Serial elapsed time: {0} ms", stopwatch.ElapsedMilliseconds);
        }
    }
}
