using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_FibonacciStockTrading
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's grab some Fibs!\n\n");

            var startingNumber = 0;
            while (startingNumber <= 0)
            {
                Console.WriteLine("Please enter a starting number find within the Fibonacci sequence:");
                int.TryParse(Console.ReadLine(), out startingNumber);
                if(startingNumber <= 0) Console.WriteLine("Please enter a valid integer.");
            }

            var numberOfFibsToReturn = 0;
            while (numberOfFibsToReturn <= 0)
            {
                Console.WriteLine("How many Fib numbers should be returned after the starting value?:");
                int.TryParse(Console.ReadLine(), out numberOfFibsToReturn);
                if (numberOfFibsToReturn <= 0) Console.WriteLine("Please enter a valid integer.");
            }

            var fibNumbers = GenerateFibs(startingNumber, numberOfFibsToReturn);

            foreach (var fibNumber in fibNumbers)
            {
                Console.WriteLine(fibNumber);
            }

            Console.WriteLine("The first available fib number after {0} was {1}",startingNumber, fibNumbers.First());
            Console.Read();
        }


        //private static bool IsFibonacci(int numberToCheck)
        //{
            
        //}


        /// <summary>
        /// Generates X number of fibonacci numbers from the given starting number.
        /// </summary>
        /// <param name="checkpoint">Starting number</param>
        /// <param name="numbersAfter">Nx'th fibonacci numbers after the starting number.</param>
        /// <returns></returns>
        private static List<int> GenerateFibs(int checkpoint, int numbersAfter)
        {
            // Fibonacci numbers equate to Fn = Fn-1 + Fn-2
            // Don't worry about the first three numbers in the sequence (0,1,1) :)
            var F_n = new List<int>();
            var F_n_1 = 1;
            var F_n_2 = 0;

            while (F_n.Count != numbersAfter + 1)
            {
                var fibNumber = F_n_1 + F_n_2;
                if(fibNumber >= checkpoint) F_n.Add(fibNumber);

                F_n_2 = F_n_1;
                F_n_1 = fibNumber;
            }

            return F_n;
        }
    }
}
