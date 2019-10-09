using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StoreApp.Test
{
    public class CustomerTest
    {
        readonly Customer customer = new Customer("Billy","Bobman");

        [Fact]
        public void Name_EmptyValue_ThrowsException()
        {

        }

        [Fact]
        public void Name_NonSpaceSeparatedValue_ThrowsException()
        {

        }

        [Fact]
        public void Name_ManySpaceSeparatedValue_ThrowsException()
        {

        }
    }

}
