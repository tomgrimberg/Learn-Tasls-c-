using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    public abstract class Product
    {
        public string Name { get; set; }
        public int Number { get; set; }

        abstract public void SayMyName();
       
    }
}
