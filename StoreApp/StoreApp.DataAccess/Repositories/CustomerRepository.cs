using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.DataAccess.Repositories
{
    public class CustomerRepository
    {
        private DoapSoapContext dbcontext;

        /// <summary>
        /// Create repository with a non-null database context
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(DoapSoapContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Repository can only be created with a valid dbcontext", nameof(context));
            }

            dbcontext = context;
        }

        /// <summary>
        /// Retrieves list of customers from db, converts them into business logic customers and returns the list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Library.Customer> GetCustomers()
        {
            return dbcontext.Customers.Select(Mapper.MapCustomer).ToList();
        }

        /// <summary>
        /// Returns a ModelCustomer if id matches, and also grabs Customer's orders; otherwise returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Library.Customer GetCustomerByID(int id)
        {
            return Mapper.MapCustomer(dbcontext.Customers.Find(id)) ?? null;
        }

        /// <summary>
        /// Returns a list of customers with names matching with the given
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Library.Customer> GetCustomersByString(string name)
        {
            string[] fullName = name.Split(' ');
            if (fullName.Length != 2)
            {
                throw new ArgumentException("Must enter first and last name.", nameof(name));
            }
            // Return customers where first name and last name match up with the string given, ignoring case
            return dbcontext.Customers
                .Where( c => c.FirstName.ToLower() == fullName[0].ToLower() && c.LastName.ToLower() == fullName[1].ToLower() )
                .Select(Mapper.MapCustomer)
                .ToList();
        }



        /*
        public IEnumerable<Library.Item> GetProductsInOrderByID(int orderID)
        {
            var items = dbcontext.OrderItems
                .Include(oi=>oi.Product)

            return items.Select()
        }
        */

        /// <summary>
        /// Search for and return a list of customers with matching given name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns> Returns a list of Count==0 if no customers found </returns>
        public IEnumerable<Library.Customer> GetCustomersByName(string name)
        {
            string[] fullName = name.Split(' ');
            IQueryable<Customers> entities;

            if (fullName.Length == 2)
            {
                entities = dbcontext.Customers.Where(c => c.FirstName == fullName[0] && c.LastName == fullName[1]);
            }
            else
            {
                return new List<Library.Customer>();
            }

            return entities.Select(Mapper.MapCustomer).ToList();

        }

        /// <summary>
        /// Adds customer to database based on the given customer object model
        /// </summary>
        /// <param name="modelCustomer"></param>
        public void AddCustomer(Library.Customer modelCustomer)
        {
            Customers modelToEntity = Mapper.MapCustomer(modelCustomer);

            // Check if customer somehow?? already exists -- but it shouldnt
            if (dbcontext.Customers.Any(c => c.CustomerId == modelToEntity.CustomerId))
            {
                Console.WriteLine("This customer already exists.");
                return;
            }
            else
            {
                // Add to database
                dbcontext.Customers.Add(modelToEntity);
            }
        }

        /// <summary>
        /// Removes a customer with matching id from the database
        /// </summary>
        /// <param name="id"></param>
        public void RemoveCustomer(int id)
        {
            Customers toBeRemoved = dbcontext.Customers.Find(id);

            dbcontext.Customers.Remove(toBeRemoved);
        }

        /// <summary>
        /// Finds customer from db to be updated, then switch values with the input'd customer values
        /// </summary>
        /// <param name="customer"></param>
        public void UpdateCustomer(Library.Customer customer)
        {
            Customers entity = dbcontext.Customers.Find(customer.CustomerId);
            Customers modelToEntity = Mapper.MapCustomer(customer);
            dbcontext.Entry(entity).CurrentValues.SetValues(modelToEntity);
        }

        /// <summary>
        /// Makes the changes to the customer persist
        /// </summary>
        public void SaveCustomer()
        {
            dbcontext.SaveChanges();
        }
    }
}
