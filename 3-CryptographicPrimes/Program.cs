using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_CryptographicPrimes
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Let's find some Primes!\n\n");

            var startNumber = 0;
            while (startNumber <= 0)
            {
                Console.WriteLine("Please enter a number to start checking primes FROM:");
                int.TryParse(Console.ReadLine(), out startNumber);
            }

            var endNumber = 0;
            while (endNumber <= 0 || endNumber <= startNumber)
            {
                Console.WriteLine("Enter a number to stop looking at primes:");
                int.TryParse(Console.ReadLine(), out endNumber);
            }

            var primeCounter = 0;
            for (var i = startNumber; i <= endNumber; i++)
            {
                if (!IsPrime(i)) continue;
                // CODE TIP: To save on nesting with brackets, throw out any non-primes
                // This section of code will only execute when there IS a prime :)
                Console.WriteLine(i);
                primeCounter++;
            }

            Console.WriteLine("There are {0} prime numbers bewteen {1} and {2}", primeCounter, startNumber, endNumber);
            Console.Read();
        }


        /// <summary>
        /// Takes an int and checks to see if it is prime or not.
        /// </summary>
        /// <param name="numberToCheck"></param>
        /// <returns></returns>
        private static bool IsPrime(int numberToCheck)
        {
            // 0 and 1 are NOT pime http://en.wikipedia.org/wiki/Prime_number
            if (numberToCheck < 2) return false;

            // Any numbers divisible by 2 can't be prime, apart from the number 2.
            // Hey look! They all just happen to be EVEN NUMBERS! :D
            if (numberToCheck%2 == 0) return numberToCheck == 2;

            // Only interested in odd numbers
            var sqrtOfNumber = (int) Math.Sqrt(numberToCheck);
            for (var i = 3; i <= sqrtOfNumber; i+= 2)
            {
                // if numberToCheck divides wihtout any remainders then we know it can't be prime!
                if(numberToCheck % i == 0) return false;
            }

            //If we've gone through all the numbers (i) we can now be certain the number IS Prime! :D
            return true;
        }
    }
}
    