using StoreApp.Library;
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

        //private int _colorID;

        // Gonna keep our products simple for now
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
        /// <remarks> 
        /// Will throw an exception if name to be set is null or empty 
        /// </remarks>
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

        /// <summary>
        /// Determines the color that product has
        /// </summary>
        public int ColorID { get; set; }

        /// <summary>
        /// A product is valid if it's name isn't null and it's price is set (not 0)
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (Name == null || Price == 0)
            {
                return false;
            }

            return true;
        }

    }

   
    
}
