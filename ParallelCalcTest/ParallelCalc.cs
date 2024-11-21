using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelCalcTest
{
    /// <summary>
    /// Calculates the sum of numbers from 1 to Max using N threads
    /// </summary>
    class ParallelCalc
    {
        public int N { get; set; }
        public long Max { get; set; }

        private long[] partialSum;

        public ParallelCalc(int n, long max)
        {
            N = n;
            Max = max;

            partialSum = new long[N];
        }

        public long StartCalc()
        {
            Thread[] threads = new Thread[N];

            for (int i = 0; i < N; i++)
            {
                int index = i; // Capture the loop variable correctly
                long start = index * Max / N;
                long end = (index + 1) * Max / N;

                // Handle the last range to include all remaining elements
                if (index == N - 1)
                {
                    end = Max;
                }

                threads[i] = new Thread(() => Calc(start, end, index));
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            long result = partialSum.Sum();

            return result;
        }

        private void Calc(long start, long end, int index)
        {
            long sum = 0;
            for (long i = start + 1; i <= end; i++)
            {
                sum += i;
            }

            partialSum[index] = sum;
        }
    }
}
