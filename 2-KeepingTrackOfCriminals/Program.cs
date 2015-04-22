using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_KeepingTrackOfCriminals
{
    class Program
    {
        static void Main(string[] args)
        {
            var serialNumber = "";

            // Get the serial number
            while (serialNumber == "")
            {
                Console.WriteLine("Please input the serial number to print:");
                serialNumber = Console.ReadLine();
            }

            // Get the number of times to print
            var timesToPrint = 0;
            while (timesToPrint == 0)
            {
                Console.WriteLine("Please enter the number of times to print {0}:", serialNumber);
                int.TryParse(Console.ReadLine(), out timesToPrint);
                if(timesToPrint==0)Console.WriteLine("That was an invalid number. Please try again."); 
            }

            // Print it out!
            Console.WriteLine("Printing {0} {1} times", serialNumber, timesToPrint);

            for (int i = 0; i < timesToPrint; i++)
            {
                Console.WriteLine("\t{0}", serialNumber);
            }

            Console.WriteLine("DONE!");
            Console.Read();
        }
    }
}
