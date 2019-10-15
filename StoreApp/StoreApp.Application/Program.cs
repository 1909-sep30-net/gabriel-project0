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
                //.UseLoggerFactory(MyLoggerFactory)
                .Options;

            using var context = new DoapSoapContext(options);


            CustomerRepository CustomerRepo = new CustomerRepository(context);
            LocationRepository LocationRepo = new LocationRepository(context);

            /* Start printing to console to guide user */


            // Going to keep this while loop condition true since that's all the application does, for now
            while (true)
            {
                Console.WriteLine("Welcome to the Doap Soap Store application! \n");

                DisplayMainMenu();

                // Read input from the user. Keep asking for input until input is valid.
                string input = Console.ReadLine();
                
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

                        // This is the order we will be modifying according to the user. If order is confirmed, use this, map it to the db entity form, then update the db with it
                        Order order = new Order();

                        // If set to true, break out of the Place Order action
                        bool orderDone = false;

                        // Who's the Customer?
                        Console.WriteLine("\nSelect the customer for this order: \n");

                        // Display list of customers in the database
                        InputParser.DisplayCustomers(CustomerRepo.GetCustomers());

                        Console.Write("\nCustomer ID: ");

                        // Select customer
                        // Read input from the user. Keep asking for input until input is valid.
                        input = Console.ReadLine();

                        int customerID;

                        // method that takes string input, tries to parse it as int. if successful, use int to find customer. if customer not found, input again.
                        while (!int.TryParse(input, out customerID) || CustomerRepo.GetCustomerByID(customerID) == null)
                        {
                            Console.WriteLine("Input invalid. Try again.");
                            Console.Write("Customer ID: ");
                            input = Console.ReadLine();
                            if (input.ToLower() == "c")
                            {
                                orderDone = true;
                                break;
                            }
                        }
                        if (orderDone)
                        {
                            break;
                        }

                        order.MyCustomer = CustomerRepo.GetCustomerByID(customerID);                       

                        Console.WriteLine("Order customer successfully set.\n");

                        // For which location?
                        Console.WriteLine("Locations List:\n");

                        // Display list of locations in the database
                        InputParser.DisplayLocations(LocationRepo.GetLocations());

                        Console.WriteLine("\nFor which location is this order being placed?\n");
                        Console.WriteLine();
                        Console.Write("Location ID: ");

                        // Select desired location
                        // Read input from the user. Keep asking for input until input is valid.
                        input = Console.ReadLine();


                        int locationID;

                        // method that takes string input, tries to parse it as int. if successful, use int to find customer. if customer not found, input again.
                        while (!int.TryParse(input, out locationID) || LocationRepo.GetLocationByID(locationID) == null)
                        {
                            Console.WriteLine("Input invalid. Try again.");
                            Console.Write("Location ID: ");
                            input = Console.ReadLine();
                            if (input.ToLower() == "c")
                            {
                                orderDone = true;
                                break;
                            }
                        }
                        if (orderDone)
                        {
                            break;
                        }

                        // Add location to order
                        order.MyLocation = LocationRepo.GetLocationByID(locationID);

                        // TODO: Put this option into a loop that only terminates once order is confirmed or user wants to cancel

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

                            switch (input)
                            {
                                // Add a product and quantity to order
                                case "a":

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
                                case "r":

                                    // Display order's list items

                                    // Select list item id

                                    // Specify quantity to remove
                                    // Handle possible quantity errors (quantity can't be negative or bring current quantity below 0)

                                    // Remove quantity of product from that list item 

                                    Console.WriteLine("Product removed!");
                                    break;

                                // Confirm the order
                                case "c":

                                    // Check if order is valid

                                    // If valid, assign order datetime

                                    // Populate Order table

                                    // Go back to main menu
                                    orderDone = true;
                                    break;

                                // Cancel the order
                                case "n":

                                    // Exits the loop
                                    orderDone = true;
                                    break;

                            }
                        } // END OF Place Order loop

                        break;

                    // Add a customer
                    case "a":

                        // Start setting up all requirements for a valid customer
                        Customer newCustomer = new Customer();

                        //while (TryParse
                        // While customer isn't valid, keep trying to create a valid customer
                        while (!newCustomer.IsValid()) 
                        {
                            Console.WriteLine("Enter customer name:");

                            // Enter info for a customer -- their name
                            input = Console.ReadLine();

                            // Try setting the name; 
                            try
                            {
                                newCustomer.Name = input;
                            }
                            //throw an exception if it's invalid and loop again since Name is still null
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }

                        // Add customer to database
                        //customers.Add(new Customer(input));
                        CustomerRepo.AddCustomer(newCustomer);
                        CustomerRepo.SaveCustomer();

                        Console.WriteLine("Customer added!");
                        
                        break;

                    // Examine Customer
                    case "c":
                        bool examiningCustomer = true;
                        while (examiningCustomer)
                        {
                            Console.WriteLine("All available customers:\n");

                            // Display all available customers
                            InputParser.DisplayCustomers(CustomerRepo.GetCustomers());

                            Customer selectCustomer = null;

                            // Select customer based on ID
                            Console.Write("\nSelect a customer by ID: ");

                            // Selected Customer ID will be parsed and stored from TryParse
                            int custID;

                            // While input or customer isn't valid, keep trying to select a valid customer
                            do
                            {
                                input = Console.ReadLine();
                                // If input is a valid int,
                                if (int.TryParse(input, out custID))
                                {
                                    // Find customer with given id
                                    selectCustomer = CustomerRepo.GetCustomerByID(custID);

                                    // If customer does not exist, print error
                                    if (selectCustomer == null)
                                    {
                                        Console.WriteLine("Input invalid. Enter valid customer ID.\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid. Enter a number.\n");
                                }
                            }
                            while (selectCustomer == null);


                            // Display all customer info: ID, Name, Orders
                            InputParser.DisplayCustomer(selectCustomer);

                            foreach(Order o in selectCustomer.OrderLog)
                            {
                                InputParser.DisplayOrder(o);
                            }

                            InputParser.DisplayOrders(selectCustomer.OrderLog);

                            Console.WriteLine("\nEnter order ID to examine");

                            int orderID;
                            Order custOrder = null;

                            // While input or order isn't valid, keep trying to select a valid order
                            do
                            {
                                input = Console.ReadLine().ToLower();
                                // If input is a valid int,
                                if (input == "c")
                                {
                                    examiningCustomer = false;
                                    break;
                                }
                                if (int.TryParse(input, out orderID))
                                {
                                    // Find customer with given id
                                    custOrder = selectCustomer.OrderLog.Find(x => x.OrderID == orderID);
                                    // If customer does not exist, print error
                                    if (custOrder == null)
                                    {
                                        Console.WriteLine("Input invalid. Enter valid order ID.\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid. Enter a number.\n");
                                }
                            }
                            while (custOrder == null);

                            InputParser.DisplayOrder(custOrder);
                        }

                        break;

                    // Examine Store Location
                    case "s":
                        bool examiningStore = true;
                        while (examiningStore)
                        {
                            // Display all available locations
                            InputParser.DisplayLocations(LocationRepo.GetLocations());

                            Console.Write("Select Location ID: ");

                            // Select desired location
                            // Read input from the user. Keep asking for input until input is valid.

                            input = Console.ReadLine();
                            if (input.ToLower() == "c")
                            {
                                examiningStore = false;
                                break;
                            }
                            while (!int.TryParse(input, out locationID) || LocationRepo.GetLocationByID(locationID) == null)
                            {
                                
                                Console.WriteLine("Input invalid. Try again.");
                                Console.WriteLine("Enter Location ID or C to cancel: ");
                                input = Console.ReadLine();
                                if (input.ToLower() == "c")
                                {
                                    examiningStore = false;
                                    break;
                                }
                            }
                            if (!examiningStore)
                            {
                                break;
                            }

                            Location myLocation = LocationRepo.GetLocationByID(locationID);

                            Console.WriteLine();
                            InputParser.DisplayLocation(myLocation);
                            Console.WriteLine("\n Inventory:");
                            InputParser.DisplayItems(myLocation.Inventory);
                            Console.WriteLine($"inventory count: {myLocation.Inventory.Count}");
                            Console.WriteLine("Location info displayed.");
                            LocationRepo.DisplayItems();
                            Console.WriteLine("now inventoryitem info displayed.");

                            // Display all location info

                            // Examine Order Log
                            // Cancel
                        }
                        break;
                }
            }
        }
    }

}
