using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public class Location
    {
        // location name (address)
        public string name { get; set; }
        // order history
        public OrderLog log { get; set; }
        // inventory

    }
}
