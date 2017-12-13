using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    public class ProductA : Product
    {
        public ProductA()
        {
            Name = "ProductA";
        }

        public override sealed void SayMyName()
        {
            Console.WriteLine("My Name is {0} And I'm Type A", Name);
        }

        public virtual void OverideTest()
        {
            Console.WriteLine("ProductA");
        }
    }
}
