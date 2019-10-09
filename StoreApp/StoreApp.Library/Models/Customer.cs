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
        /// Constructor for instantiating Customer with first and last name
        /// </summary>
        /// <param name="first">First name</param>
        /// <param name="last">Last name</param>
        public Customer(string first, string last)
        {
            _firstName = first;
            _lastName = last;
        }

        /// <summary>
        /// Id used to uniquely identify customer in database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Property that returns the first and last name of the customer.
        /// </summary>
        public string Name
        {
            get
            {
                return _firstName + " " + _lastName;
            }

            set 
            {
                // Split input into strings separated by spaces. If valid input, then firstLastNames should hold two strings (first and last name)
                string[] firstLastNames = value.Split(' ');
                if (firstLastNames.Length != 2)
                {
                    throw new ArgumentException("Name should two words", nameof(value));
                }

                // Check if either string in firstLastNames is an empty string
                // Split(' ') can return a string[] with an empty string value if given something like "hi "
                foreach (string item in firstLastNames)
                {
                    if (item == "")
                    {
                        throw new ArgumentException("Name should be two words separated by a space", nameof(value));
                    }
                }

                // If no exceptions have been thrown at this point, should be safe to assign
                _firstName = firstLastNames[0];
                _lastName = firstLastNames[1];
            }
        
    }




        /// <summary>
        /// Holds the history of customer's orders
        /// </summary>
        public List<Order> OrderLog { get; set; } = new List<Order>();


    }

}
