using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Location
    {
        /// <summary>
        /// Unique identifier to be used for database organization
        /// </summary>
        public int Id { get; set; }

        private string _name;

        /// <summary>
        /// Name of particular location
        /// </summary>
        public string Name
        { 
            get
            { 
                return _name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name should not be null.", nameof(value));
                }
                else if (value.Length <= 0)
                {
                    throw new ArgumentException("Name must be non-empty string.", nameof(value));
                }
                else
                {
                    _name = value;
                }
            }
        }
       
        
        /// <summary>
        /// History of orders for this location
        /// </summary>
        public OrderLog log { get; set; }

        /// <summary>
        /// List of items and quantity of how much this location currently stores
        /// </summary>
        public List<Tuple<Product, int>> inventory { get; set; } = new List<Tuple<Product, int>>();
    }
}
