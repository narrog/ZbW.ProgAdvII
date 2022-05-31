using System;
using System.Collections.Generic;

namespace _1_Fibonacci {
    public class Math {
        // TODO: Implementiere Methode Fibonacci
        public static IEnumerable<int> Fibonacci(int n)
        {
            var prepreFib = 0;
            yield return prepreFib;
            var preFib = 1;
            yield return preFib;
            if (n > 2)
            {
                for (int i = 3; i <= n; i++)
                {
                    var fib = prepreFib + preFib;
                    prepreFib = preFib;
                    preFib = fib;
                    yield return fib;
                }
            }
        }
    }

    class Program {
        static void Main(string[] args) {
            foreach (int i in Math.Fibonacci(8))
            {
                Console.WriteLine("{0} ", i);
            }

            Console.ReadKey();
        }
    }
}