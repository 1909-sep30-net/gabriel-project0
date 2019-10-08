using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Customer
    {
        
        private string _firstName;
        private string _lastName;
        private int _id;

        /// <summary>
        /// Construct a Customer with a first and last name
        /// </summary>
        /// <param name="first"></param>
        /// <param name="last"></param>
        public Customer(string first, string last, int Id)
        {
            _firstName = first;
            _lastName = last;
            _id = Id;

        }

 
        public int Id { get; }

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


         /// <summary>
         /// 
         /// </summary>
        public OrderLog Log { get; }


    }

}
