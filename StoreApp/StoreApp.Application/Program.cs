using System;
using System.Collections.Generic;
using System.Linq;
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
            OrderRepository OrderRepo = new OrderRepository(context);

            /* Start printing to console to guide user */


            // Going to keep this while loop condition true since that's all the application does, for now
            while (true)
            {
                Console.WriteLine("\nWelcome to the Doap Soap Store application!\n");

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

                        // This is the order we will be modifying according to the user.
                        // If order is confirmed, use this, map it to the db entity form, then update the db with it
                        Order order = new Order();

                        // Who's the Customer?
                        Console.WriteLine("\nChoose customer for this order.\n('C' to cancel)\n");

                        // Get list of available customers
                        var customersP = CustomerRepo.GetCustomers().ToList();

                        // Display list of customers in the database
                        Console.WriteLine("Available Customers -----------\n");
                        foreach (Customer customer in customersP)
                        {
                            Console.WriteLine($"{customer.Name}");
                        }
                        Console.WriteLine("\n\t-----------");

                        // Select customer
                        Console.WriteLine("\nType in customer name to select.\n('C' to cancel)\n");

                        // Loop for selecting a customer until user inputs "c"
                        while (true)
                        {
                            List<Customer> customerP = new List<Customer>();
                            input = Console.ReadLine();
                            if (input.ToLower() == "c")
                            {
                                break;
                            }
                            try
                            {
                                customerP = CustomerRepo.GetCustomersByString(input).ToList();
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            // If try block succeeded and customerP has a list in it, list customers
                            if (customerP.Count > 0)
                            {
                                Console.WriteLine("Found customers -----------\n");
                                for (int i=0;i<customerP.Count;i++)
                                {
                                    Console.WriteLine($"#{i+1}: {customerP[i]}");
                                }
                                Console.WriteLine("\n\t-----------\n");

                                Console.WriteLine("Choose customer by number.\n('C' to cancel)\n");
                                while (true)
                                {
                                    input = Console.ReadLine();
                                    int customerNum;
                                    if (input.ToLower() == "c")
                                    {
                                        break;
                                    }
                                    if (int.TryParse(input,out customerNum))
                                    {
                                        // If input is a valid number and also a number in the customer list, continue
                                        if (customerNum > 0 && customerNum <= customerP.Count)
                                        {
                                            // Now we have our customer object! And we can feed this to the order
                                            Customer selectedCustomer = customerP[customerNum-1];
                                            order.MyCustomer = selectedCustomer;

                                            // Continue with order
                                                // Add a product
                                                    // List products available in location
                                                    // Specify product and quantity
                                                    // Check with inventory of location
                                        }
                                        else
                                        {
                                            Console.WriteLine("Pick a valid number from the list.\n('C' to cancel)\n");
                                        }
                                    } 
                                    else // If input was not 'C' or a valid int, print error and try again
                                    {
                                        Console.WriteLine("Not a valid option. Must be a number from the list or 'C' to cancel.");
                                    }
                                }
                            }
                            else // If customerP list is empty 
                            {
                                Console.WriteLine("Customer not found.");
                            }
                        }
                        // Read input from the user. Keep asking for input until input is valid.
                        input = Console.ReadLine();

                        int customerID;

                        // method that takes string input, tries to parse it as int. if successful, use int to find customer. if customer not found, input again.
                        while (!int.TryParse(input, out customerID) || CustomerRepo.GetCustomerByID(customerID) == null)
                        {
                            if (input.ToLower() == "q")
                            {
                                
                                break;
                            }
                            Console.WriteLine("Input invalid. Try again.");
                            Console.Write("Customer ID: ");
                            input = Console.ReadLine();

                        }
                        if (input.ToLower() == "q")
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
                                break;
                            }
                        }
                        if (input.ToLower() == "c")
                        {
                            break;
                        }

                        // Add location to order
                        order.MyLocation = LocationRepo.GetLocationByID(locationID);

                        // TODO: Put this option into a loop that only terminates once order is confirmed or user wants to cancel

                        // Loop until user is done modifying order
                        while (true)
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
                                    

                                    break;

                                // Cancel the order
                                case "n":

                                    // Exits the loop
                                    
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

                        Console.WriteLine("All available customers:\n");

                        // Retrieve a list of business logic customers
                        var customersC = CustomerRepo.GetCustomers().ToList();

                        // If customers list is empty
                        if (customersC.Count < 1)
                        {
                            // Break out of ExamineCustomer action and go back to main menu
                            Console.WriteLine("No customers to display.\n");
                            break;
                        }
                        else
                        {
                            // Display all available customers
                            InputParser.DisplayCustomers(customers);

                            // Selected Customer ID will be parsed and stored from TryParse
                            int custID;

                            // Selected customer will be stored here
                            Customer selectCustomer = null;

                            // Select customer based on ID
                            Console.Write("\nSelect a customer by ID: ");

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

                            Console.WriteLine("Customer info: ");

                            // Display all customer info: ID, Name, Orders

                            // Obtain orders from particular customer
                            var customerOrders = OrderRepo.GetOrdersWithProductsByCustomerID(selectCustomer.CustomerId).ToList();

                            InputParser.DisplayOrders(customerOrders);
                        }
                        break;

                    // Examine Store Locations
                    case "s":

                        var locations = LocationRepo.GetLocations().ToList();

                        // If there are no locations, break out of case
                        if (locations.Count < 1)
                        {
                            Console.WriteLine("No locations to view.\n");
                            break;
                        }
                        while (true)
                        {
                            // Display all available locations
                            Console.WriteLine("\nAvailable Locations:\n");
                            for (int i = 0; i < locations.Count; i++)
                            {
                                Console.WriteLine($"{i+1}:\t{locations[i].Name}");
                            }

                            Console.WriteLine("\nSelect Location by #: \n('C' to cancel)\n");
                            input = Console.ReadLine();
                            if (input == "c")
                            {
                                break;
                            }
                            int locationIdx;

                            if (int.TryParse(input, out locationIdx)) // If input can be parsed into an int
                            {
                                // And the int is in the range [1-locations.Count]
                                if (locationIdx > 0 && locationIdx <= locations.Count)
                                {
                                    // Grab location
                                    Location location = locations[locationIdx-1];

                                    Console.WriteLine($"Selected location:\t{location.Name}\n");
                                    bool viewingLocation = true;
                                    while (viewingLocation)
                                    {
                                        // Choose inventory or order history to view
                                        Console.Write("\nView [I]nventory or [O]rder History: \n('C' to cancel)\n");
                                        input = Console.ReadLine();

                                        // If input is i, view inventory
                                        if (input.ToLower() == "i")
                                        {

                                        }
                                        // If input is o, view order history
                                        else if (input.ToLower() == "o") 
                                        {
                                            Console.WriteLine();
                                            // Print location details + order history

                                            // Get location's orders
                                            var locationOrders = LocationRepo.GetOrders(location.Id).ToList();
                                            
                                            Console.WriteLine($"{location.Name}'s Order History\n");

                                            // Print orders by index
                                            for (int i=0;i<locationOrders.Count;i++)
                                            {
                                                Console.WriteLine("-------------");
                                                Order currentOrder = locationOrders[i];

                                                Console.WriteLine($"Order: {i+1}\n");
                                                Console.WriteLine($"Customer: {locationOrders[i].MyCustomer.Name} |"
                                                                + $" Time Ordered:\t{locationOrders[i].MyTime}\n");

                                                var orderItems = OrderRepo.GetOrderItemsByOrderID(currentOrder.OrderID);
                                                decimal revenue = 0;
                                                // PRINT THE ORDER ITEMS IN THE ORDER
                                                foreach (Item item in orderItems)
                                                {
                                                    Console.WriteLine($"-\n\tProduct: {item.Product.Name}\n\tQuantity: {item.Quantity}\n-");
                                                    revenue += item.Product.Price * item.Quantity;
                                                }
                                                Console.Write($"Total Revenue: ");
                                                Console.WriteLine("${0:N2}", revenue);

                                                Console.WriteLine("-------------");
                                                    
                                            }
                                            if (locationOrders.Count == 0)
                                            {
                                                Console.WriteLine("No orders from this location. :(\nSomeone should probably " +
                                                                   "send a sternly written letter to their manager.\n");
                                            }
                                            
                                        } 
                                        else if (input.ToLower() == "c")
                                        {
                                            break;
                                        }
                                        else // if neither, print error for invalid input
                                        {
                                            Console.WriteLine("Invalid input. \n\tEnter 'I' to view inventory\n\t'O' to view order history\n'C' to cancel\n");
                                        }
                                        
                                    } // End of looking at specific location
                                }
                                else // But the input isn't within range
                                {
                                    Console.WriteLine("Pick a number in the list of locations.\n");
                                }
                            }
                            // If input cannot be parsed into int, send msg to console and list again
                            else
                            {
                                Console.WriteLine("Invalid input. Enter a number.\n");
                            }
                        } // End of Location listing loop




                        // Select desired location based on index
                        // Read input from the user. Keep asking for input until input is valid.
                        /*
                        input = Console.ReadLine();
                        while(input.ToLower() != "c" )
                        {

                        }
                        while (!int.TryParse(input, out locationID) || LocationRepo.GetLocationByID(locationID) == null)
                        {
                            if (input.ToLower() == "c")
                            {
                                break;
                            }
                            Console.WriteLine("Input invalid. Try again.");
                            Console.WriteLine("Select Location by #: \n(or 'C' to cancel)");
                            input = Console.ReadLine();

                        }
                        if (input.ToLower() == "c")
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
                        */
                        // Display all location info

                        // Examine Order Log
                        // Cancel
                        
                        break;
                }
            }
        }
    }

}
