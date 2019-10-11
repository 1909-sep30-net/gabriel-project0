using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Application
{
    public static class InputParser
    {
        // Take these out later lol theyre just to satisfy some compiler errors for now
        public static List<Location> locations;
        public static List<Customer> customers;

        /// <summary>
        /// Verifies if the specific action is valid
        /// </summary>
        /// <param name="optionType">The type of options being handled.</param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns>Returns the string being checked as a char.</returns>
        public static bool IsValidActionInput(int optionType, string input, out char output)
        {
            // Check if null was passed in as argument
            if (input == null)
            {
                throw new ArgumentNullException("Input cannot be null.", nameof(input));
            }

            // We want input to be case-insensitive for convenience
            input = input.ToLower();

            // Send input out as a char
            output = input[0];


            // If our input is not any of the available options, we can safely assume 
            //  that action input is not valid, return false

            switch (optionType)
            {
                // Options for application task
                case 1:
                    if (input == "p" || input == "a" || input == "c" || input == "s")
                    {
                        return true;
                    }
                    break;

                // Options for modifying an order
                case 2:
                    if (input == "a" || input == "r" || input == "c" || input == "n")
                    {
                        return true;
                    }
                    break;
            }


            return false;
        }

        // Checks if customer name is in list of existing customers
        static bool IsValidCustomerSelectionByName(string name)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                if (customers[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        // Selects customer from list by a given name
        static Customer SelectCustomerByName(string name)
        {
            if (IsValidCustomerSelectionByName(name))
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    if (customers[i].Name == name)
                    {
                        return customers[i];
                    }
                }
            }
            return null;
        }

        // Checks if location with name exists in location list
        static bool IsValidLocationSelectionByName(string name)
        {
            for (int i = 0; i < locations.Count; i++)
            {
                if (locations[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        // Returns an existing location with the given name
        static Location SelectLocationByName(string name)
        {
            if (IsValidLocationSelectionByName(name))
            {
                for (int i = 0; i < locations.Count; i++)
                {
                    if (locations[i].Name == name)
                    {
                        return locations[i];
                    }
                }
            }
            return null;
        }
    }
}
