using System;
using StoreApp.Library;

namespace StoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Dope Soap store interface! \n");

            Console.WriteLine("What would you like to do?\n");

            Console.WriteLine("P - Place an Order");
            Console.WriteLine("A - Add New Customer");
            Console.WriteLine("C - Examine Customer");
            Console.WriteLine("S - Examine Store Location\n");

            Console.WriteLine("Please enter a letter to choose an action: ");

            // Read input from the user. Keep asking for input until input is valid.
            string input = Console.ReadLine();
            char output;
            while (!IsValidActionInput(input, out output))
            {
                Console.WriteLine("Input was not valid. \nPlease enter one of the following: 'P', 'A', 'C', 'S'");
                input = Console.ReadLine();
            }

            // Once user has entered valid input, continue to the action specified
            switch (output)
            {
                case 'p':
                    break;

                case 'a':
                    break;

                case 'c':
                    break;

                case 's':
                    break;

            }
        }

        static bool IsValidActionInput(string input, out char output)
        {
            // Check if null was passed in as argument
            if (input == null)
            {
                throw new ArgumentNullException("Input cannot be null.",nameof(input));
            }
            // We want input to be case-insensitive for convenience
            input = input.ToLower();

            // Send input out as a char
            output = input[0];

            // If our input is not any of the available options, we can safely assume 
            //  that action input is not valid, return false
            if (input == "p" || input == "a" || input == "c" || input == "s")
            {
                return true;
            }

            return false;
        }
    }
}
