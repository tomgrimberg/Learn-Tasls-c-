using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    public class Client
    {
        private readonly BaseFactory myFactory;
        public Client(BaseFactory myFactory)
        {
            this.myFactory = myFactory;
        }
        
        public void sayClientProductNameA()
        {
            myFactory.createProduct("A").SayMyName();
        }

        public void sayClientProductNameB()
        {
            myFactory.createProduct("B").SayMyName();
        }
    }
}
