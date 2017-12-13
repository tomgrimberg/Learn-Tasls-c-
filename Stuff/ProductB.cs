using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    public class ProductB : Product
    {
        public ProductB()
        {
            Name = "ProductB";
        }
        public override void SayMyName()
        {
            Console.WriteLine("My Name is {0} And I'm Type B", Name);
        }
    }
}
