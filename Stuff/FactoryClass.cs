using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    public class FactoryClass :BaseFactory
    {
        override public Product createProduct(string type)
        {
            switch (type)
            {
                case "A": return new ProductA();
                    break;
                case "B": return new ProductB();
                    break;
                default:  return null;
                    break;
            }
        }
    }
}
