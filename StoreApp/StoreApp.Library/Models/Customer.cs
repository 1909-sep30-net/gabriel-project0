using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{

    public class Customer
    {
        private string _firstName;
        private string _lastName;

        /// <summary>
        /// Id used to uniquely identify customer in database
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Handles getting and setting first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                // Error handling for invalid name settings
                ValidateName(value);

                // Always capitalize first letter and lowercase the rest
                _firstName = value[0].ToString().ToUpper() + value.Substring(1).ToLower();
            }
        }

        /// <summary>
        /// Handles getting and setting last name
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                // Error handling for invalid name settings
                ValidateName(value);

                // Always capitalize first letter and lowercase the rest
                _lastName = value[0].ToString().ToUpper() + value.Substring(1).ToLower();
            }
        }

        /// <summary>
        /// Shorthand way of setting and getting the full name of the customer
        /// </summary>
        public string Name
        {
            get
            {
                return _firstName + " " + _lastName;
            }

            set
            {
                // Split up input and delimit by spaces
                string[] firstLastNames = value.Split(' ');

                // If split doesn't return a two length string, then input wasn't a first and last name
                if (firstLastNames.Length != 2)
                {
                    throw new ArgumentException("Name should be two words", nameof(value));
                }

                // Use properties to handle any other errors and set
                FirstName = firstLastNames[0];
                LastName = firstLastNames[1];
            }
        }

        /// <summary>
        /// Holds the history of customer's orders
        /// </summary>
        public List<Order> OrderLog { get; set; } = new List<Order>();

        /// <summary>
        /// Error handling for inserting a name
        /// </summary>
        /// <param name="value"></param>
        private void ValidateName(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Name cannot be null", nameof(value));
            }
            if (value.Length < 1)
            {
                throw new ArgumentException("Name must be at least one letter", nameof(value));
            }
        }
        public bool IsValid()
        {
            // If the first or last name isn't set, this isn't a valid customer
            if (FirstName == null || LastName == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns an order from customer's orderlog, or null if not found
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Order GetOrder(int ID)
        {
            return OrderLog.Find(x => x.OrderID == ID);
        }
    }

}
