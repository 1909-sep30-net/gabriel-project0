using StoreApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Product
    {
        /* Private backing fields for properties */

        private string _name;

        private decimal _price;

        private int _colorID;

        // Gonna keep our products simple for now lol
        //private string _size;


        /* ------------------------------------- */

        /// <summary>
        /// Unique ID for every product type
        /// </summary>
        /// <remarks> 
        /// Products with same names can have different sizes, colors, etc, 
        /// so need to have another value for unique identity. 
        /// </remarks>
        public int ID { get; set; }

        /// <summary>
        /// Name of the soap product
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
        /// Price of the soap product
        /// </summary>
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price must be non-zero, non-negative value", nameof(value));
                }

                _price = value;
            }
        }

        public int ColorID { get; set; }

    }

   
    
}
