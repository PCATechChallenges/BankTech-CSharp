using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_CallCentreSpam
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneList = "PhoneList.txt";
            var numbers = File.ReadLines(phoneList);
            var spamCount = 0;
            var goodCount = 0;

            foreach (var phoneNumber in numbers)
            {
                var isSpam = phoneNumber.Contains("SPAM");
                if (isSpam)
                {
                    spamCount++;
                }
                else
                {
                    goodCount++;
                }

                Console.WriteLine(isSpam ? "SPAM" : "OK");
            }
            Console.WriteLine("\n\n{2} numbers processed\n{0} SPAM numbers found\n{1} numbers were OK", spamCount, goodCount, spamCount + goodCount);
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
