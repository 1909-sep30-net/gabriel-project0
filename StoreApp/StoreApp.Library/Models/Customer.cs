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
                if (value == null)
                {
                    throw new ArgumentNullException("Name cannot be null", nameof(value));
                }
                if (value.Length < 1)
                {
                    throw new ArgumentException("Name must be at least one letter", nameof(value));
                }

                // Always capitalize first letter and lowercase the rest
                string newName = value[0].ToString().ToUpper() + value.Substring(1).ToLower();
                _lastName = newName;
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
                if (value == null)
                {
                    throw new ArgumentNullException("Name cannot be null", nameof(value));
                }
                if (value.Length < 1)
                {
                    throw new ArgumentException("Name must be at least one letter", nameof(value));
                }

                // Always capitalize first letter and lowercase the rest
                string newName = value[0].ToString().ToUpper() + value.Substring(1).ToLower();
                _lastName = newName;
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
                string[] firstLastNames = value.Split(' ');

                if (firstLastNames.Length != 2)
                {
                    throw new ArgumentException("Name should two words", nameof(value));
                }

                FirstName = firstLastNames[0];
                LastName = firstLastNames[1];
            }
        }

        /// <summary>
        /// Holds the history of customer's orders
        /// </summary>
        public List<Order> OrderLog { get; } = new List<Order>();


    }

}
