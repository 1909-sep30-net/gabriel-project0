﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Location
    {
        public Location(int id, string name)
        {
            Id = id;
            Name = name;
        }
        /// <summary>
        /// Unique identifier to be used for database organization
        /// </summary>
        public int Id { get; set; }

        // Backing field for Name property
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
        /// Holds the history of customer's orders
        /// </summary>
        /// <remarks>
        /// Instantiate class with an empty log list
        /// </remarks>
        public List<Order> OrderLog { get; set; } = new List<Order>();

        /// <summary>
        /// List of items and their quantity available at this particular location
        /// </summary>
        /// <remarks>
        /// Instantiate class with an empty inventory list
        /// </remarks>
        public List<Tuple<Product, int>> Inventory { get; set; } = new List<Tuple<Product, int>>();


    }
}
