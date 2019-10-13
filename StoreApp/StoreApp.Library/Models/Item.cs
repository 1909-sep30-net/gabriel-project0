using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Item
    {
        // Private backing fields
        private Product _product;
        private int _quantity;

        public Product Product
        {
            get
            {
                return _product;
            }
            set
            {
                // Cannot add a null product
                if (value == null)
                {
                    throw new ArgumentNullException("Product cannot be null.", nameof(value));
                }

                _product = value;
            }
        }

        public int Quantity 
        {
            get
            {
                return _quantity;
            }
            set
            {
                // Validate quantity
                if (value < 0)
                {
                    throw new ArgumentException("Quantity cannot be negative.", nameof(value));
                }

                _quantity = value;
            }
        }

    }
}
