using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Customer
    {
        public Customer(string first, string last)
        {
            if (first.Length > 0)
            {
                _firstName = first;
            }
            
            _lastName = last;
        }

        private string _firstName;
        private string _lastName;
        private int _id;
 
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

        }

        public void UpdateName(string first, string last)
        {

            if (first == null)
            {
                throw new ArgumentNullException("Name should not be null.", nameof(first));
            }
            else if (first.Length <= 0)
            {
                throw new ArgumentException("Name must be non-empty string.", nameof(first));
            }
            else
            {
                _firstName = first;
            }
        }


         /// <summary>
         /// 
         /// </summary>
        public OrderLog Log { get; set; }


    }

}
