using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    public class CustomerTest
    {
        // Set up customer object for every test to use
        Customer customer = new Customer
        {
            FirstName = "Billy",
            LastName = "Bob",
        };

        [Fact]
        public void FirstName_Null_ThrowsNullArgumentException()
        {
            string newName = null;

            Assert.Throws<ArgumentNullException>( () => customer.FirstName = newName );
        }

        [Fact]
        public void LastName_Null_ThrowsNullArgumentException()
        {
            string newName = null;

            Assert.Throws<ArgumentNullException>( () => customer.LastName = newName );
        }

        [Fact]
        public void Name_Null_ThrowsNullArgumentException()
        {
            string newName = null;

            Assert.Throws<ArgumentNullException>( () => customer.Name = newName );
        }

       
        [Theory]
        [InlineData("Billy Bobby")]
        [InlineData("Scarlet Johanssons")]
        [InlineData("Hey Jude")]
        public void Name_TwoWordsSeparatedBySpace_NameStoredCorrectly(string newName)
        {

            customer.Name = newName;

            Assert.Equal(newName, customer.Name);
        }

        [Fact]
        public void Name_NonSpaceSeparatedValue_ThrowsArgumentException()
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

    }

}
