using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    public class ProductStore
    {
        //The big difference between a singleton and a static class with a bunch of static methods is that :
        //singletons can implement interfaces (or derive from useful base classes, although that's less common),
        //so you can pass around the singleton as if it were "just another" implementation.

        private static ProductStore _instance = null;
        private static readonly object _lock = new object();
        private ProductStore()
        {

        }
        public static ProductStore Instance
        {
            get
            {   //Double Check for Thread Safe
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ProductStore();
                        }
                    }
                }
                return _instance;

            }
        }

        /// <summary> ANOTHER WAY TO CREATE SINGELTON WITHOUT LOCK

        // public sealed class Singleton
        // {

        //    private static readonly Singleton instance = new Singleton();

        //    Explicit static constructor to tell C# compiler
        //    not to mark type as beforefieldinit

        //    static Singleton()
        //    {
        //    }

        //    private Singleton()
        //    {
        //    }

        //    public static Singleton Instance
        //    {
        //        get
        //        {
        //            return instance;
        //        }
        //    }
        // }
        /// </summary>


        public Product Build(IProductBuilder builder)
        {
            builder.setName("bla");
            builder.setNumber(10);
            return builder.GetProduct();
        }
    }
}
