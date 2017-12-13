using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    class ProductC : ProductA
    {
        public new void SayMyName()
        {
            Console.WriteLine("I'm Type C");
        }

        public void OverideTest()
        {
            Console.WriteLine("ProductC");
        }
    }
}
