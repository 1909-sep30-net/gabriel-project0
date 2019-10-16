using System;
using NLog;
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

        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        private static readonly NLog.ILogger s_logger = LogManager.GetCurrentClassLogger();


        public static void DisplayMainMenu()
        {
            Console.WriteLine("What would you like to do?\n");

            Console.WriteLine("P - Place an Order");
            Console.WriteLine("A - Add New Customer");
            Console.WriteLine("C - Examine Customer");
            Console.WriteLine("S - Examine Store Location\n");

            Console.WriteLine("Q - Quit\n");

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

            Console.WriteLine("\nWelcome to the Doap Soap Store application!\n");

            // Going to keep this while loop condition true since that's all the application does, for now
            while (true)
            {
                s_logger.Info("Main Menu Accessed");


                DisplayMainMenu();

                // Read input from the user. Keep asking for input until input is valid.
                string input = Console.ReadLine();
                
                while (!InputParser.IsValidActionInput((int)Menus.Main, input))
                {
                    Console.WriteLine("\nInput was not valid. \nPlease enter one of the following: 'P', 'A', 'C', 'S', 'Q'\n");

                    DisplayMainMenu();

                    input = Console.ReadLine();
                }

                // Once user has entered valid input, continue to the action specified
                switch (input.ToLower())
                {
                    // Place an order
                    case "p":
                        bool PlacingOrder = true;
                        s_logger.Info("Placing order");

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
                            s_logger.Info("Selecting customer");

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
                                s_logger.Warn(ex);
                                continue;
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



                                            // Select location from the list
                                            while (PlacingOrder)
                                            {
                                                Console.WriteLine("Choose location for this order.\n");

                                                // Display all available locations
                                                Console.WriteLine("\nAvailable Locations:\n");
                                                for (int i = 0; i < locationsP.Count; i++)
                                                {
                                                    Console.WriteLine($"{i + 1}:\t{locationsP[i].Name}");
                                                }

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

                                                        // Clears the order's cart
                                                        order.ProductList.Clear();

                                                        // Load location necessities

                                                        // Display list of products from current location
                                                        var locationInventory = LocationRepo.GetInventoryItemsById(location.Id).ToList();

                                                        // Use this inventory repo to do business logic stuff as we try adding to order
                                                        InventoryRepository invenRepo = new InventoryRepository(locationInventory);

                                                        // Add a product
                                                        while (PlacingOrder)
                                                        {
                                                            //bool EditingOrder = true;
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
                                                                        Console.Write($"#{i+1}\t{item.Product.Name} | {item.Product.ColorName} | {item.Quantity} | ");
                                                                        Console.WriteLine("Price: ${0:N2}", item.Product.Price);
                                                                    }
                                                                    if (locationInventory.Count == 0)
                                                                    {
                                                                        Console.WriteLine("\nStore is all out of stock!\n");
                                                                        continue;
                                                                    }
                                                                    // Select Product
                                                                    while (SelectingItem)
                                                                    {
                                                                        Console.Write("Select Product #: ");
                                                                        input = Console.ReadLine();
                                                                        
                                                                        int productIdx;
                                                                        // Try getting an int input to select product #
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
                                                                                    s_logger.Info(ex);
                                                                                    continue;
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
                                                                                        // Validate setting the quantity
                                                                                        try
                                                                                        {
                                                                                            orderItem.Quantity = quantityP;
                                                                                        }
                                                                                        catch (ArgumentException ex)
                                                                                        {
                                                                                            Console.WriteLine(ex.Message);
                                                                                            s_logger.Warn(ex);
                                                                                            // Ask for quantity again
                                                                                            continue;
                                                                                        }

                                                                                        // Validate removing from the inventory
                                                                                        try
                                                                                        {
                                                                                            invenRepo.Remove(selectedProduct, quantityP);
                                                                                        }
                                                                                        catch (ArgumentException ex)
                                                                                        {
                                                                                            s_logger.Warn(ex);
                                                                                            Console.WriteLine(ex.Message);
                                                                                            // Ask for quantity again
                                                                                            continue;
                                                                                        }
                                                                                        
                                                                                        // Validate adding the item to the order
                                                                                        try
                                                                                        {
                                                                                            order.Add(orderItem);
                                                                                            
                                                                                        }
                                                                                        catch (ArgumentException ex)
                                                                                        {
                                                                                            s_logger.Warn(ex);
                                                                                            Console.WriteLine(ex.Message);
                                                                                        }
                                                                                        s_logger.Info("Item added to order");
                                                                                        SelectingItem = false;
                                                                                        Console.WriteLine($"\n{selectedProduct.Name}, | {selectedProduct.ColorName} Quantity: {quantityP} added to order.\n");
                                                                                        break;
                                                                                    }
                                                                                    else // Input is not even an int
                                                                                    {
                                                                                        Console.WriteLine("Please enter a number.");
                                                                                        s_logger.Warn("Please enter a number.");
                                                                                    }
                                                                                }

                                                                            } // Input is not within valid range
                                                                            else
                                                                            {
                                                                                Console.WriteLine($"{input} not a valid list number.\n");
                                                                                s_logger.Warn($"{input} not a valid list number.\n");
                                                                            }

                                                                        }
                                                                        else // Input was not valid int
                                                                        {
                                                                            Console.WriteLine("Pick a product # from list to choose.");
                                                                        }
                                                                    } // End of Selecting Product Loop

                                                                } // Done Adding to order

                                                                // Confirm order
                                                                if (option == 2)
                                                                {

                                                                    order.MyTime = DateTime.Now;
                                                                    // Update associated objects / tables
                                                                    // location inventory
                                                                    // orders
                                                                    // orderitems
                                                                        
                                                                    OrderRepo.AddOrder(order);
                                                                    OrderRepo.SaveChanges();

                                                                    // Add orderitems based on order coming from db
                                                                    Order updatedOrder = OrderRepo.GetMostRecentOrder();

                                                                    //OrderRepo.AddOrderItems(order.ProductList, updatedOrder);
                                                                    LocationRepo.UpdateInventory(locationInventory,order.MyLocation);
                                                                    OrderRepo.SaveChanges();

                                                                    Console.WriteLine("Order has been placed!");
                                                                    s_logger.Info("Order has been placed!");
                                                                    PlacingOrder = false;
                                                                
                                                                }

                                                                // View Current order
                                                                if (option == 3)
                                                                {
                                                                    Console.WriteLine("Currently in order: \n");
                                                                    foreach (Item item in order.ProductList)
                                                                    {
                                                                        Console.WriteLine($"{item.Product.Name}, {item.Product.ColorName} Quantity: {item.Quantity}");
                                                                    }
                                                                    if (order.ProductList.Count == 0)
                                                                    {
                                                                        Console.WriteLine("Order currently empty. :(\n Add products to your order!\n");
                                                                    }
                                                                    Console.WriteLine("\n---------------\n");
                                                                }
                                                                
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Invalid input. Enter a number to select from option list.");
                                                                s_logger.Warn("Invalid input. Enter a number to select from option list.");
                                                            }
                                                        } // End of Adding To / Confirming order loop
                                                    }
                                                    else // But the input isn't within range
                                                    {
                                                        Console.WriteLine("Pick a number in the list of locations.\n");
                                                        s_logger.Warn("Pick a number in the list of locations.\n");
                                                    }
                                                }
                                                // If input cannot be parsed into int, send msg to console and list again
                                                else
                                                {
                                                    Console.WriteLine("Invalid input. Enter a number or 'C'.\n");
                                                    s_logger.Warn("Invalid input. Enter a number or 'C'.\n");
                                                }
                                            } // End of selecting from list loop                                         
                                        }
                                        else // Input an int but not a valid option
                                        {
                                            Console.WriteLine("Pick a valid number from the list.\n('C' to go back)\n");
                                            s_logger.Warn("Pick a valid number from the list. ('C' to go back)");
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
                                s_logger.Warn(ex);
                            }
                        }

                        // Add customer to database
                        CustomerRepo.AddCustomer(newCustomer);
                        CustomerRepo.SaveCustomer();

                        Console.WriteLine("Customer added!");
                        s_logger.Info($"Customer {newCustomer.Name} added");
                        
                        break;

                    // Examine Customer
                    case "c":
                        bool ExaminingCustomer = true;
                        while (ExaminingCustomer)
                        {

                            // Select customer

                            // List of customers that may pop up from search
                            List<Customer> customerC = new List<Customer>();

                            Console.WriteLine("\nSearch for customer by name.\n('C' to go back)\n");
                            Console.Write("Customer Name: ");
                            input = Console.ReadLine();
                            if (input.ToLower() == "c")
                            {
                                break;
                            }
                            try
                            {
                                customerC = CustomerRepo.GetCustomersByString(input).ToList();
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                                s_logger.Warn(ex);
                                continue;
                            }
                            // If count is greater than 0, there were some results
                            if (customerC.Count > 0)
                            {
                                while (ExaminingCustomer)
                                {
                                    // Display matching customers found
                                    Console.WriteLine("\nFound customers -----------\n");
                                    for (int i = 0; i < customerC.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}:\t{customerC[i].Name}");
                                    }
                                    Console.WriteLine("\n\t-----------\n");

                                    Console.WriteLine("Choose customer by number.\n('C' to go back)\n('Q' to quit to main menu)\n");
                                    Console.Write("\nCustomer #: ");
                                    input = Console.ReadLine();
                                    int customerNum;
                                    // Break if input C
                                    if (input.ToLower() == "c")
                                    {
                                        break;
                                    }
                                    if (input.ToLower() == "q")
                                    {
                                        ExaminingCustomer = false;
                                        break;
                                    }
                                    // Try parsing input as int
                                    if (int.TryParse(input, out customerNum))
                                    {
                                        // If input is a valid number and also a number in the customer list, continue
                                        if (customerNum > 0 && customerNum <= customerC.Count)
                                        {
                                            // Now we have our customer object! Now we can choose to view their orders
                                            Customer selectedCustomer = customerC[customerNum - 1];

                                            // Get location's orders
                                            var customerOrders = OrderRepo.GetOrdersWithProductsByCustomerID(selectedCustomer.CustomerId).ToList();

                                            Console.WriteLine($"{selectedCustomer.Name}'s Order History\n");

                                            // Print orders by index
                                            for (int i = 0; i < customerOrders.Count; i++)
                                            {
                                                Console.WriteLine("-------------");
                                                Order currentOrder = customerOrders[i];

                                                Console.WriteLine($"Order: {i + 1}\n");
                                                Console.WriteLine($"Location: {customerOrders[i].MyLocation.Name} |"
                                                                + $" Time Ordered:\t{customerOrders[i].MyTime}\n");

                                                var orderItems = OrderRepo.GetOrderItemsByOrderID(currentOrder.OrderID);

                                                // sum of the product price * quantity of every item in order
                                                decimal revenue = 0;

                                                // PRINT THE ORDER ITEMS IN THE ORDER
                                                foreach (Item item in orderItems)
                                                {
                                                    Console.WriteLine($"-\n\tProduct: {item.Product.Name}, {item.Product.ColorName}\n\tQuantity: {item.Quantity}\n-");
                                                    revenue += item.Product.Price * item.Quantity;
                                                }
                                                Console.Write($"Total Cost: ");
                                                Console.WriteLine("${0:N2}", revenue);

                                                Console.WriteLine("-------------");

                                            }
                                            s_logger.Info($"{selectedCustomer.Name} order's viewed");

                                            if (customerOrders.Count == 0)
                                            {
                                                Console.WriteLine("No orders from this customer. :(\nYet.\n");
                                            }
                                        } 
                                        else
                                        {
                                            Console.WriteLine("Number must be within range of the list.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Must be number or 'C'");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No customer found!");
                            }


                        } // End of Selecting customer loop
                        // If "q" was used to quit the case, reset value of input so that it doesn't carry you out of the loop running the program
                        input = "";
                        break; // End of Examine Customer case

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
                                            s_logger.Info($"{location.Name} inventory viewed");

                                            var locationInventory = LocationRepo.GetInventoryItemsById(location.Id).ToList();
                                            Console.WriteLine($"{location.Name}'s Inventory");
                                            Console.WriteLine("---------------------------");
                                            foreach ( Item item in locationInventory )
                                            {
                                                Console.Write($"{item.Product.Name} | {item.Product.ColorName} | {item.Quantity} |");
                                                Console.WriteLine("Price: ${0:N2}",item.Product.Price);
                                            }
                                            Console.WriteLine("---------------------------");

                                        }
                                        // If input is o, view order history
                                        else if (input.ToLower() == "o") 
                                        {
                                            s_logger.Info($"{location.Name} order history viewed");

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
                                                    Console.WriteLine($"-\n\tProduct: {item.Product.Name}, {item.Product.ColorName}\n\tQuantity: {item.Quantity}\n-");
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
                if (input == "q")
                {
                    break;
                }
            }
        }
    }

}
