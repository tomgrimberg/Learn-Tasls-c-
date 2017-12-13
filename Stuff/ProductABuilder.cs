using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    public class ProductABuilder : IProductBuilder
    {
        private Product MyProduct = new ProductA();
        
        public void setName(string name)
        {
            MyProduct.Name = name;
        }

        public void setNumber(int number)
        {
            MyProduct.Number = number;
        }

        public Product GetProduct()
        {
            return MyProduct;
        }
    }
}
