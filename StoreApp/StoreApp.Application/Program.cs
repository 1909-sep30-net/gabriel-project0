using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.DataAccess.Entities;
using StoreApp.DataAccess.Repositories;
using StoreApp.Library;

namespace StoreApp.Application
{
    public class Program
    {
        enum Menus : int
        {
            Main,
            Order,
        };

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public static void DisplayMainMenu()
        {
            Console.WriteLine("What would you like to do?\n");

            Console.WriteLine("P - Place an Order");
            Console.WriteLine("A - Add New Customer");
            Console.WriteLine("C - Examine Customer");
            Console.WriteLine("S - Examine Store Location\n");

            Console.Write("Please enter a letter to choose an action: ");
        }

        public static void Main(string[] args)
        {
            var connectionString = SecretConfig.ConfigString;

            DbContextOptions<DoapSoapContext> options = new DbContextOptionsBuilder<DoapSoapContext>()
                .UseSqlServer(connectionString)
                .UseLoggerFactory(MyLoggerFactory)
                .Options;

            using var context = new DoapSoapContext(options);


            CustomerRepository CustomerRepo = new CustomerRepository(context);

            /* Start printing to console to guide user */


            // Going to keep this while loop condition true since that's all the application does, for now
            while (true)
            {
                Console.WriteLine("Welcome to the Doap Soap Store application! \n");

                DisplayMainMenu();

                // Read input from the user. Keep asking for input until input is valid.
                string input = Console.ReadLine();
                char output;
                
                while (!InputParser.IsValidActionInput((int)Menus.Main, input))
                {
                    Console.WriteLine("\nInput was not valid. \nPlease enter one of the following: 'P', 'A', 'C', 'S'\n");

                    DisplayMainMenu();

                    input = Console.ReadLine();
                }
                input.ToLower();

                // Once user has entered valid input, continue to the action specified
                switch (input)
                {
                    // Place an order
                    case "p":

                        Order order = new Order();

                        // Who's the Customer?
                        Console.WriteLine("\nSelect the customer for this order: \n");

                        // Display list of customers in the database
                        InputParser.DisplayCustomers(CustomerRepo.GetCustomers());

                        Console.WriteLine();
                        Console.Write("Customer ID: ");

                        // Select customer
                        // Read input from the user. Keep asking for input until input is valid.
                        input = Console.ReadLine();

                        /*
                        while (!IsValidCustomerSelectionByName(input))
                        {
                            Console.WriteLine("Input was not valid. Please enter the name of a customer.\n");
                            Console.WriteLine();
                            Console.Write("Customer name: ");
                            input = Console.ReadLine();
                            Console.WriteLine();
                        }
                        */

                        // Add customer to order
                        //order.MyCustomer = SelectCustomerByName(input);

                        // For which location?
                        Console.WriteLine("For which location is this order being placed?");

                        // Display list of locations in the database

                        Console.WriteLine();
                        Console.Write("Location name: ");

                        // Select desired location
                        // Read input from the user. Keep asking for input until input is valid.
                        input = Console.ReadLine();

                        /*
                        while (!IsValidLocationSelectionByName(input))
                        {
                            Console.WriteLine("Input was not valid. \nPlease enter the name of a location.");
                            Console.WriteLine();
                            Console.Write("Location name: ");
                            input = Console.ReadLine();
                        }
                        */

                        // Add location to order
                        //order.MyLocation = SelectLocationByName(input);

                        // TODO: Put this option into a loop that only terminates once order is confirmed or user wants to cancel
                        bool orderDone = false;

                        // Loop until user is done modifying order
                        while (!orderDone)
                        {

                            // What's the order?
                            Console.WriteLine("What would you like to do to your order?\n");
                            Console.WriteLine("A - Add Products");
                            Console.WriteLine("R - Remove Products");
                            Console.WriteLine("C - Confirm Order");
                            Console.WriteLine("N - Cancel Order\n");
                            input = Console.ReadLine();
                            /*
                            while (!IsValidActionInput(2, input, out output))
                            {
                                Console.WriteLine("\nInput was not valid. \n " +
                                                  "Please enter one of the following: 'A', 'R', 'C', 'N' \n");

                                // What's the order?
                                Console.WriteLine("\nWhat would you like to do to your order?\n");
                                Console.WriteLine("A - Add Products");
                                Console.WriteLine("R - Remove Products");
                                Console.WriteLine("C - Confirm Order");
                                Console.WriteLine("N - Cancel Order\n");
                                input = Console.ReadLine();
                            }
                            */

                            switch (output)
                            {
                                // Add a product and quantity to order
                                case 'a':

                                    /* ~TODO~ Fill out these comments */

                                    // Display products available to be added
                                    // DisplayProducts()

                                    // Select product (by ID?) to be added
                                    // SelectProduct(int ID)

                                    // Specify quantity to add
                                    // Handle possible quantity errors (quantity can't be negative or ridiculously high)

                                    // Add selected product to order with quantity

                                    Console.WriteLine("Product added!");
                                    break;

                                // Remove a product from the order
                                case 'r':

                                    // Display order's list items

                                    // Select list item id

                                    // Specify quantity to remove
                                    // Handle possible quantity errors (quantity can't be negative or bring current quantity below 0)

                                    // Remove quantity of product from that list item 

                                    Console.WriteLine("Product removed!");
                                    break;

                                // Confirm the order
                                case 'c':

                                    // Check if order is valid

                                    // If valid, assign order datetime

                                    // Populate Order table

                                    // Go back to main menu
                                    orderDone = true;
                                    break;

                                // Cancel the order
                                case 'n':

                                    // Exits the loop
                                    orderDone = true;
                                    break;

                            }
                        } // END OF Place Order loop

                        break;

                    // Add a customer
                    case "a":

                        /* TODO: Fix all this lazy input handling */

                        Console.WriteLine("Enter customer name:");
                        // Enter info for a customer -- their name
                        input = Console.ReadLine();

                        // Add customer to database
                        //customers.Add(new Customer(input));

                        Console.WriteLine("Customer added!");
                        
                        break;

                    // Examine Customer
                    case "c":
                        // Display all available customers

                        // Select customer based on ID

                        // Display all customer info

                            // Examine Order Log
                            // Cancel

                        break;

                    // Examine Store Location
                    case "s":
                        // Display all available locations

                        // Select location based on ID

                        // Display all location info

                            // Examine Order Log
                            // Cancel
                        break;

                }
            }
        }
    }

}
