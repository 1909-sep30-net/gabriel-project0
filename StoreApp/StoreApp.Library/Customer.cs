using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    class Customer
    {
        private Name name;

        public List<Order> OrderHistory;
    }

    struct Name
    {
        string first;
        string last;
    }
}
