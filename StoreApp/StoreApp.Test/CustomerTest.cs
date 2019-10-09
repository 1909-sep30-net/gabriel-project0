using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    public class CustomerTest
    {
        readonly Customer customer = new Customer();

        [Fact]
        public void Name_Null_ThrowsNullException()
        {
            string newName = null;

            Assert.Throws<ArgumentNullException>(() => customer.Name = newName);
        }

        [Fact]
        public void Name_EmptyValue_ThrowsException()
        {
            string newName = "";

            Assert.Throws<ArgumentException>( () => customer.Name = newName );
        }

        [Fact]
        public void Name_NonSpaceSeparatedValue_ThrowsException()
        {
            string newName = "BillBobby";
            Assert.Throws<ArgumentException>(() => customer.Name = newName);
        }

        [Fact]
        public void Name_ManySpaceSeparatedValue_ThrowsException()
        {
            string newName = "Billy Bobby Jean Bib";
            Assert.Throws<ArgumentException>(() => customer.Name = newName);
        }

        [Fact]
        public void Name_TwoWordsSeparatedBySpace_ValidName()
        {
            string newName = "Billy Bobby";
            customer.Name = newName;
            Assert.Equal(customer.Name, newName);
        }
    }

}
