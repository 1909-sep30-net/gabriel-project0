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

        public List<Library.Customer> GetCustomers()
        {
            var entities = dbcontext.Customers;

            return Mapper.MapCustomers(entities);
        }

        /// <summary>
        /// Returns a ModelCustomer if id matches, otherwise returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Library.Customer GetCustomerByID(int id)
        {
            return Mapper.MapCustomer(dbcontext.Customers.Find(id)) ?? null;
        }

        /// <summary>
        /// Search for and return a list of customers with matching given name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Library.Customer> GetCustomersByName(string name)
        {
            string[] fullName = name.Split(' ');

            IQueryable<Customers> entities;
            if (fullName.Length <= 0)
            {
                throw new ArgumentException();
            }
            if (fullName.Length == 1)
            {
                entities = dbcontext.Customers.Where(c => c.FirstName == fullName[0] || c.LastName == fullName[0]);
            }
            if (fullName.Length == 2)
            {
                entities = dbcontext.Customers.Where(c => c.FirstName == fullName[0] && c.LastName == fullName[1]);
            } else
            {
                throw new ArgumentException("Can only search by partial or full name", nameof(name));
            }

            // Create and populate list of customers
            List<Library.Customer> resultingModelList = new List<Library.Customer>();

            foreach (Customers item in entities)
            {
                resultingModelList.Add(Mapper.MapCustomer(item));
            }
            return resultingModelList;
        }

        public void AddCustomer(Library.Customer modelCustomer)
        {
            Customers modelToEntity = Mapper.MapCustomer(modelCustomer);

            if (dbcontext.Customers.Any(c => c.CustomerId == modelToEntity.CustomerId))
            {
                Console.WriteLine("This customer already exists.");
                return;
            }
            else
            {
                dbcontext.Customers.Add(modelToEntity);
            }
        }

        public void RemoveCustomer(int id)
        {
            Customers toBeRemoved = dbcontext.Customers.Find(id);

            dbcontext.Remove(toBeRemoved);
        }

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
