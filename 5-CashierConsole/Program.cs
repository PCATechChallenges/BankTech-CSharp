using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_CashierConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bank Tech's Cashier System\n" +
                              "######################################\n");

            while (true)
            {
                // Get the starting balance first
                var balance = 0.0d;
                while (balance <= 0)
                {
                    Console.WriteLine("Please enter the customer's starting balance:");
                    double.TryParse(Console.ReadLine(), out balance);
                    if(balance < 0) Console.WriteLine("There is no overdraft on this account. Please use whole numbers");
                }

                // Now an infinite loop to make multiple transactions on one account.
                while (true)
                {
                    var action = "";
                    while (string.IsNullOrEmpty(action))
                    {
                        Console.WriteLine(
                        "Current balance is: £{0}\n Press 'A' to add more funds or 'S' to subtract from the balance or 'X' to exit:", balance);
                        action = Console.ReadLine();
                    }

                    if (action.ToUpper() == "X") break;
                    
                    switch (action.ToUpper())
                    {
                        // Add funds   
                        case "A":
                            AddToBalance(ref balance);
                            break;

                        case "S":
                            SubtractFromBalance(ref balance);
                            break;
                        default:
                            Console.WriteLine("That command was not recognised. Please try again.");
                            break;
                    }
                }

                break;
            }

            // If we get this far, the user MUST have pressed 'X' or 'x' in one of the input screens.
            Console.WriteLine("Session complete.\n Have a nice day :)");
            Console.Read();
        }


        /// <summary>
        /// Takes a reference to the balance variable being passed and Adds funds to it directly.
        /// </summary>
        /// <param name="balance"></param>
        private static void AddToBalance(ref double balance)
        {
            Console.WriteLine("How much would you like to add to the current balance?");
            var sumToAdd = 0.0d;
            while (sumToAdd <= 0.0d)
            {
                double.TryParse(Console.ReadLine(), out sumToAdd);
                if(sumToAdd <= 0.0d) Console.WriteLine("Please enter a valid number to ADD to the balance.");
            }
            balance += sumToAdd;
            Console.WriteLine("Added £{0}", sumToAdd);
        }


        /// <summary>
        /// Takes a reference to the balance variable being passed and Subtracts funds from it directly.
        /// </summary>
        /// <param name="balance"></param>
        private static void SubtractFromBalance(ref double balance)
        {
            Console.WriteLine("How much would you like to subtract from the current balance?");
            var sumToSubtract = 0.0d;
            while (sumToSubtract <= 0.0d)
            {
                double.TryParse(Console.ReadLine(), out sumToSubtract);
                if (sumToSubtract <= 0.0d) Console.WriteLine("Please enter a valid number to SUBTRACT to the balance.");
            }
            balance -= sumToSubtract;
            Console.WriteLine("Subtracted £{0}", sumToSubtract);
        }

    }
}
