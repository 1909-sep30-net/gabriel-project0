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

        private string _size;

        private string _color;

        private int _quantity;

        /* ------------------------------------- */

        /// <summary>
        /// Initialize all important values by default with default item properties
        /// </summary>
        public Product()
        {
            _name = "Default Soap";
            _price = 1.00M;
            _size = "smolboy";
            _color = "black";
        }


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
        /// <summary>
        /// Size of the soap product
        /// </summary>
        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                // Must set size to valid size name
                if (value != "smolboy" && value != "midkid" && value != "madman")
                {
                    throw new ArgumentException("Size must be smolboy, midkid, or madman.", nameof(value));
                }

                _size = value;
            }
        }

        /// <summary>
        /// Color of the soap product
        /// </summary>
        public string Color 
        { 
            get
            {
                return _color;
            }
            set
            {
                // Must set color to black, white or green.
                if (value != "black" && value != "white" && value != "green")
                {
                    throw new ArgumentException("Color must be black, white, or green.", nameof(value));
                }

                _color = value;
            }
        }

        /// <summary>
        /// Amount of product currently stored
        /// </summary>
        public int Quantity
        {
            get
            {
                return _quantity;
            }

            
            set
            {
                // Can only set to positive numbers
                if (value <= 0)
                {
                    throw new ArgumentException("Quantity cannot be set to or less than zero.", nameof(value));
                }
                _quantity = value;
            }
        }
    }
}
