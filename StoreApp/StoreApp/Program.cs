using System;
using System.Collections.Generic;
using StoreApp.Library;

namespace StoreApp
{
    class Program
    {
        static List<Customer> customers;
        static List<Location> locations;

        static void Main(string[] args)
        {
            // Instantiate list of available customers in place of DB
            customers = new List<Customer>();

            // Instantiate list of available locations in place of DB
            locations = new List<Location>();


            /* Start printing to console to guide user */

            Console.WriteLine("Welcome to the Doap Soap store interface! \n");

            // Going to keep this while loop condition true since that's all the application does, for now
            while (true)
            {

                Console.WriteLine("What would you like to do?\n");

                Console.WriteLine("P - Place an Order");
                Console.WriteLine("A - Add New Customer");
                Console.WriteLine("C - Examine Customer");
                Console.WriteLine("S - Examine Store Location\n");

                Console.WriteLine("Please enter a letter to choose an action: ");

                // Read input from the user. Keep asking for input until input is valid.
                string input = Console.ReadLine();
                char output;
                while (!IsValidActionInput(1, input, out output))
                {
                    Console.WriteLine("Input was not valid. \nPlease enter one of the following: 'P', 'A', 'C', 'S'");
                    input = Console.ReadLine();
                }

                // Once user has entered valid input, continue to the action specified
                switch (output)
                {
                    case 'p':

                        Order order = new Order();

                        // Who's the Customer?
                        Console.WriteLine("Which customer is creating the order?");

                        // Display list of customers in the database
                        foreach (Customer c in customers)
                        {
                            Console.WriteLine(c.Name);
                        }
                        Console.WriteLine();
                        Console.Write("Customer name: ");

                        // Select customer
                        // Read input from the user. Keep asking for input until input is valid.
                        input = Console.ReadLine();

                        while (!IsValidCustomerSelectionByName(input))
                        {
                            Console.WriteLine("Input was not valid. Please enter the name of a customer.\n");
                            Console.WriteLine();
                            Console.Write("Customer name: ");
                            input = Console.ReadLine();
                            Console.WriteLine();
                        }

                        // Add customer to order
                        order.MyCustomer = SelectCustomerByName(input);

                        // For which location?
                        Console.WriteLine("For which location is this order being placed?");

                        // Display list of locations in the database
                        foreach (Location l in locations)
                        {
                            Console.WriteLine(l.Name);
                        }
                        Console.WriteLine();
                        Console.Write("Location name: ");

                        // Select desired location
                        // Read input from the user. Keep asking for input until input is valid.
                        input = Console.ReadLine();

                        while (!IsValidLocationSelectionByName(input))
                        {
                            Console.WriteLine("Input was not valid. \nPlease enter the name of a location.");
                            Console.WriteLine();
                            Console.Write("Location name: ");
                            input = Console.ReadLine();
                        }

                        // Add location to order
                        order.MyLocation = SelectLocationByName(input);

                        // TODO: Put this option into a loop that only terminates once order is confirmed or user wants to cancel
                        bool orderDone = false;

                        // Loop until user is done modifying order
                        while (!orderDone)
                        {

                            // What's the order?
                            Console.WriteLine("Do you want to [A]dd products, [R]emove products, [C]onfirm, or Ca[n]cel your order?");
                            input = Console.ReadLine();

                            while (!IsValidActionInput(2, input, out output))
                            {
                                Console.WriteLine("Input was not valid. \nPlease enter one of the following: 'A', 'R', 'C', 'N'");
                                input = Console.ReadLine();
                            }

                            switch (output)
                            {
                                // Add a product and quantity to order
                                case 'a':
                                    break;

                                // Remove a product from the order
                                case 'r':
                                    break;

                                // Confirm the order
                                case 'c':
                                    break;

                                // Cancel the order
                                case 'n':
                                    orderDone = true;
                                    break;

                            }
                        }

                        break;

                    case 'a':
                        break;

                    case 'c':
                        break;

                    case 's':
                        break;

                }
            }
        }

        /// <summary>
        /// Verifies if the specific action is valid
        /// </summary>
        /// <param name="optionType">The type of options being handled.</param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns>Returns the string being checked as a char.</returns>
        static bool IsValidActionInput(int optionType, string input, out char output)
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

            switch(optionType)
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
