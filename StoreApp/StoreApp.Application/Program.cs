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

                // Once user has entered valid input, continue to the action specified
                switch (input.ToLower())
                {
                    // Place an order
                    case "p":
                        bool PlacingOrder = true;

                        Console.WriteLine("\nPlace an order!");

                        Console.WriteLine("\nChoose customer for this order.\n");

                        // This is the order we will be modifying according to the user.
                        // If order is confirmed, use this, map it to the db entity form, then update the db with it
                        Order order = new Order();

                        // Get list of available customers
                        var customersP = CustomerRepo.GetCustomers().ToList();

                        // Display list of customers in the database
                        Console.WriteLine("Available Customers -----------\n");
                        foreach (Customer customer in customersP)
                        {
                            Console.WriteLine($"{customer.Name}");
                        }
                        Console.WriteLine("\n\t-----------");

                        // Loop for selecting a customer until user inputs "c"
                        while (PlacingOrder)
                        {
                            // Select customer
                            Console.WriteLine("\nType in customer name to select.\n('C' to go back)\n");
                            Console.Write("Customer Name: ");
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

                                // Loop while choosing a customer from the list
                                while (PlacingOrder)
                                {
                                    Console.WriteLine("\nFound customers -----------\n");
                                    for (int i = 0; i < customerP.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}:\t{customerP[i].Name}");
                                    }
                                    Console.WriteLine("\n\t-----------\n");

                                    Console.WriteLine("Choose customer by number.\n('C' to go back)\n");
                                    Console.Write("Customer #: ");
                                    input = Console.ReadLine();
                                    int customerNum;
                                    // Break if input C
                                    if (input.ToLower() == "c")
                                    {
                                        break;
                                    }
                                    // If input is a valid number
                                    if (int.TryParse(input, out customerNum))
                                    {
                                        // If input is a valid number and also a number in the customer list, continue
                                        if (customerNum > 0 && customerNum <= customerP.Count)
                                        {
                                            // Now we have our customer object! And we can feed this to the order
                                            Customer selectedCustomer = customerP[customerNum-1];
                                            order.MyCustomer = selectedCustomer;

                                            Console.WriteLine("Customer successfully applied to order.\n");

                                            // Choose location

                                            var locationsP = LocationRepo.GetLocations().ToList();

                                            Console.WriteLine("Choose location for this order.\n");

                                            // Display all available locations
                                            Console.WriteLine("\nAvailable Locations:\n");
                                            for (int i = 0; i < locationsP.Count; i++)
                                            {
                                                Console.WriteLine($"{i + 1}:\t{locationsP[i].Name}");
                                            }

                                            // Select from the list
                                            while (PlacingOrder)
                                            {
                                                Console.WriteLine("\nSelect Location by #: \n('C' to go back)\n");
                                                Console.Write("Selection: ");
                                                input = Console.ReadLine();
                                                if (input.ToLower() == "c")
                                                {
                                                    break;
                                                }
                                                int locationIdx;

                                                // If input can be parsed into an int
                                                if (int.TryParse(input, out locationIdx)) 
                                                {
                                                    // And the int is in the range [1-locations.Count]
                                                    if (locationIdx > 0 && locationIdx <= locationsP.Count)
                                                    {
                                                        // Grab location
                                                        Location location = locationsP[locationIdx - 1];

                                                        // Put into current order
                                                        order.MyLocation = location;
                                                        Console.WriteLine($"\nOrder will be for Location: {location.Name}!\n");

                                                        // Load location necessities

                                                        // Display list of products from current location
                                                        var locationInventory = LocationRepo.GetInventoryItemsById(location.Id).ToList();

                                                        // Use this inventory repo to do business logic stuff as we try adding to order
                                                        InventoryRepository invenRepo = new InventoryRepository(locationInventory);

                                                        // Add a product
                                                        while (PlacingOrder)
                                                        {
                                                            bool EditingOrder = true;
                                                            // Display order options
                                                            Console.WriteLine("What would you like to do to your order?\n");
                                                            Console.WriteLine("1:\tAdd Products\n" +
                                                                              "2:\tConfirm Order\n" +
                                                                              "3:\tView Current Order\n" +
                                                                              "C:\tGo Back\n" +
                                                                              "Q:\tQuit to Main Menu\n");
                                                            Console.Write("Option: ");
                                                            input = Console.ReadLine();
                                                            int option;
                                                            if (input.ToLower() == "c")
                                                            {
                                                                break;
                                                            }
                                                            if (input.ToLower() == "q")
                                                            {
                                                                PlacingOrder = false;
                                                                break;
                                                            }

                                                            // Parse int answers
                                                            if (int.TryParse(input, out option))
                                                            {
                                                                // ADD PRODUCT
                                                                if (option == 1)
                                                                {
                                                                    // Item to populate and eventually insert into order
                                                                    Item orderItem = new Item();
                                                                    bool SelectingItem = true;

                                                                    // Display location's inventory
                                                                    Console.WriteLine($"{location.Name}'s Inventory");
                                                                    Console.WriteLine("---------------------------");
                                                                    for (int i=0;i<locationInventory.Count;i++)
                                                                    {
                                                                        Item item = locationInventory[i];
                                                                        Console.Write($"#{i+1}\t{item.Product.Name} | {item.Quantity} | ");
                                                                        Console.WriteLine("Price: ${0:N2}", item.Product.Price);
                                                                    }

                                                                    // Select Product
                                                                    while (SelectingItem)
                                                                    {
                                                                        Console.Write("Select Product #: ");
                                                                        input = Console.ReadLine();
                                                                        
                                                                        int productIdx;
                                                                        if (int.TryParse(input,out productIdx))
                                                                        {
                                                                            // Input is within valid range
                                                                            if ( productIdx > 0 && productIdx <= locationInventory.Count )
                                                                            {
                                                                                Product selectedProduct = locationInventory[productIdx-1].Product;
                                                                                try
                                                                                {
                                                                                    orderItem.Product = selectedProduct;
                                                                                }
                                                                                catch( Exception ex )
                                                                                {
                                                                                    Console.WriteLine(ex.Message);
                                                                                }
                                                                                // Choose quantity
                                                                                while(true)
                                                                                {
                                                                                    Console.Write("Choose Quantity: ");
                                                                                    int quantityP;
                                                                                    input = Console.ReadLine();
                                                                                    // Input is a valid int
                                                                                    if (int.TryParse(input, out quantityP))
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            invenRepo.Remove(selectedProduct,quantityP);
                                                                                        }
                                                                                        catch (ArgumentException ex)
                                                                                        {
                                                                                            Console.WriteLine(ex.Message);
                                                                                        }
                                                                                        order.ProductList.Add(new Item { Product = selectedProduct, Quantity = quantityP});
                                                                                        SelectingItem = false;
                                                                                        Console.WriteLine($"{selectedProduct.Name}, Quantity {quantityP} added to order.");
                                                                                        break;
                                                                                    }
                                                                                    else // Input is not even an int
                                                                                    {
                                                                                        Console.WriteLine("Please enter a number.");
                                                                                    }
                                                                                }

                                                                            } // Input is not within valid range
                                                                            else
                                                                            {
                                                                                Console.WriteLine("Input not a valid list number.\n");
                                                                            }

                                                                        }
                                                                        else // Input was not valid int
                                                                        {
                                                                            Console.WriteLine("Pick a product # from list to choose.");
                                                                        }
                                                                    }

                                                                } // Done Adding to order
                                                                // Confirm order
                                                                if (option == 2)
                                                                {

                                                                }

                                                                // View Current order
                                                                if (option == 3)
                                                                {
                                                                    Console.WriteLine("Currently in order: \n");
                                                                    foreach (Item item in order.ProductList)
                                                                    {
                                                                        Console.WriteLine($"{item.Product.Name}, {item.Quantity}");
                                                                    }
                                                                    Console.WriteLine("\n---------------\n");
                                                                }
                                                                
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Invalid input. Enter a number to select from option list.");
                                                            }
                                                        } // End of Adding To / Confirming order loop
                                                    }
                                                    else // But the input isn't within range
                                                    {
                                                        Console.WriteLine("Pick a number in the list of locations.\n");
                                                    }
                                                }
                                                // If input cannot be parsed into int, send msg to console and list again
                                                else
                                                {
                                                    Console.WriteLine("Invalid input. Enter a number or 'C'.\n");
                                                }
                                            } // End of selecting from list loop                                         
                                        }
                                        else // Input an int but not a valid option
                                        {
                                            Console.WriteLine("Pick a valid number from the list.\n('C' to go back)\n");
                                        }
                                    } 
                                    else // If input was not 'C' or a valid int, print error and try again
                                    {
                                        Console.WriteLine("Not a valid option. Must be a number from the list or 'C' to go back.");
                                    }
                                }
                            }
                            else // If customerP list is empty 
                            {
                                Console.WriteLine("Customer not found.");
                            }

                        }
                        
                        // END OF Place Order loop

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
                            //InputParser.DisplayCustomers(customers);

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

                            Console.WriteLine("\nSelect Location by #: \n('C' to go back)\n");
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
                                        Console.Write("\nView [I]nventory or [O]rder History: \n('C' to go back)\n");
                                        input = Console.ReadLine();

                                        // If input is i, view inventory
                                        if (input.ToLower() == "i")
                                        {
                                            var locationInventory = LocationRepo.GetInventoryItemsById(location.Id).ToList();
                                            Console.WriteLine($"{location.Name}'s Inventory");
                                            Console.WriteLine("---------------------------");
                                            foreach ( Item item in locationInventory )
                                            {
                                                Console.Write($"{item.Product.Name} | {item.Quantity} | ");
                                                Console.WriteLine("Price: ${0:N2}",item.Product.Price);
                                            }
                                            Console.WriteLine("---------------------------");

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
                                            Console.WriteLine("Invalid input. \n\tEnter 'I' to view inventory\n\t'O' to view order history\n'C' to go back\n");
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
                        
                        break;
                }
            }
        }
    }

}
