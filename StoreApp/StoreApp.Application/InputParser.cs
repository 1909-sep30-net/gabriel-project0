using StoreApp.DataAccess.Entities;
using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static bool IsValidActionInput(int optionType, string input)
        {
            // Check if null was passed in as argument
            if (string.IsNullOrEmpty(input))
            {
                return false;

                // Do I throw this here???
                throw new ArgumentNullException("Input cannot be null.", nameof(input));
            }

            // We want input to be case-insensitive for convenience
            input = input.ToLower();

            // Send input out as a char

            // If our input is not any of the available options, we can safely assume 
            //  that action input is not valid, return false

            switch (optionType)
            {
                // Options for application task
                case 0:
                    if (input == "p" || input == "a" || input == "c" || input == "s")
                    {
                        return true;
                    }
                    break;

                // Options for modifying an order
                case 1:
                    if (input == "a" || input == "r" || input == "c" || input == "n")
                    {
                        return true;
                    }
                    break;
            }


            return false;
        }

        // Checks if customer name is in list of existing customers
        public static void CustomerSelection(DoapSoapContext context)
        {
            List<Customers> listOfCustomers;
            DisplayCustomers(context, out listOfCustomers);

            String input = Console.ReadLine();


            if (CustomerInList(listOfCustomers, input))
            {

            }
        }

        public static bool CustomerInList(List<Customers> customers, string ID)
        {
            int intID = Convert.ToInt32(ID);

            foreach (Customers customer in customers)
            {
                if (customer.CustomerId == intID)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Given a list of customers, display their information
        /// </summary>
        /// <param name="customers"></param>
        public static void DisplayCustomers(List<Library.Customer> customers)
        {
            foreach (Library.Customer customer in customers)
            {
                Console.WriteLine($"ID: {customer.CustomerId} | NAME: {customer.FirstName + " " + customer.LastName}");
            }
        }

        public static void DisplayCustomers(DoapSoapContext context, out List<Customers> outlist)
        {
            var customerEntities = context.Customers.ToList();
            foreach (Customers customer in customerEntities)
            {
                Console.WriteLine($"ID: {customer.CustomerId} | NAME: {customer.FirstName + " " + customer.LastName}");
            }
            outlist = customerEntities;
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
