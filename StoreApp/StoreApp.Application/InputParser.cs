using StoreApp.DataAccess.Entities;
using StoreApp.DataAccess.Repositories;
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

        /// <summary>
        /// Check if customer is in the list
        /// </summary>
        /// <param name="cRepo"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool CustomerInList(CustomerRepository cRepo, string ID)
        {
            int intID;
            if (int.TryParse(ID, out intID))
            {
                if(cRepo.GetCustomerByID(intID) != null)
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Input invalid. Try again.");
                return false;
            }

            Console.WriteLine("Customer not found. Try again.");
            return false;
        }

        public static bool LocationInList(LocationRepository lRepo, string ID)
        {
            int intID;
            if (int.TryParse(ID, out intID))
            {
                if (lRepo.GetLocationByID(intID) != null)
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Input invalid. Try again.");
                return false;
            }

            Console.WriteLine("Location not found. Try again.");
            return false;
        }

        /// <summary>
        /// Display details of a location
        /// </summary>
        /// <param name="location"></param>
        public static void DisplayLocation(Library.Location location)
        {
            Console.WriteLine($"ID:\t{location.Id} | Name:\t {location.Name}");
        }

        /// <summary>
        /// Displays a list of locations by ID and Name
        /// </summary>
        /// <param name="locations"></param>
        public static void DisplayLocations(List<Library.Location> locations)
        {
            foreach (Library.Location location in locations)
            {
                DisplayLocation(location);
            }
        }

        

        /// <summary>
        /// Displays a single item
        /// </summary>
        /// <param name="item"></param>
        public static void DisplayItem(Library.Item item)
        {
            Console.WriteLine($"Name:\t{item.Product.Name} | Quantity:\t{item.Quantity}\n");
        }

        public static void DisplayItems(List<Library.Item> items)
        {
            foreach (Library.Item item in items)
            {
                DisplayItem(item);
            }
        }

        /// <summary>
        /// Display a single model order
        /// </summary>
        /// <param name="order"></param>
        public static void DisplayOrder(Library.Order order)
        {
            if (order != null)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"OrderID:\t{order.MyTime}\n");
                Console.WriteLine($"Customer:\t{order.MyCustomer.Name} | Location:\t{order.MyLocation.Name} | Time Placed:\t{order.MyTime}\n");
                Console.WriteLine(); //TODO display order's items
                DisplayItems(order.ProductList);
            }
        }

        /// <summary>
        /// Display a list of model orders
        /// </summary>
        public static void DisplayOrders(List<Library.Order> orders)
        {
            foreach (Library.Order order in orders)
            {
                DisplayOrder(order);
            }
        }

        public static void DisplayCustomer(Library.Customer customer)
        {
            Console.WriteLine($"ID:\t{customer.CustomerId} | NAME:\t{customer.FirstName + " " + customer.LastName}");
        }

        /// <summary>
        /// Given a list of customers, display their information
        /// </summary>
        /// <param name="customers"></param>
        public static void DisplayCustomers(List<Library.Customer> customers)
        {
            foreach (Library.Customer customer in customers)
            {
                DisplayCustomer(customer);
            }
        }

        /*
        public static void DisplayCustomers(DoapSoapContext context, out List<Customers> outlist)
        {
            var customerEntities = context.Customers.ToList();
            foreach (Customers customer in customerEntities)
            {
                Console.WriteLine($"ID: {customer.CustomerId} | NAME: {customer.FirstName + " " + customer.LastName}");
            }
            outlist = customerEntities;
        }
        */

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
