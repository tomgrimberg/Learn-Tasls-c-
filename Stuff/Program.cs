using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Stuff
{
    //Delegate
    public delegate int myDelegate(int a, int b);

    class Program
    {
        // Just A Reminder //
        // When Returning an List Object Or reference Object you should create and return an IEnumarble Or another Object instead so the private list or Object would be protected from change

        //for unknown number of parameters functionName(params int[] list)

        //Clousures: variables are passed by Reference (they are "shared variables" - should be syncronized or locked in threads)
        //When a Clousure is inside a Delegate then ..
        //..all the clousure variables point to variables inside the new class created by the compiler for the delegate.

        //@ string literal (avoids escape characters)
        //$ string creates a string by replacing the contained expressions with the ToString represenations of the expressions’ results
        //var someDir = "a";
        //Console.WriteLine($@"c:\{someDir}\b\c");
        //output : c:\a\b\c
        //var name = SAM;
        //var msg = $"hello, {name.ToLower()}";
        //Console.WriteLine(msg); // hello, sam

        //Toggling boolean variable
        //boolVer = !boolVer;


        static void Main(string[] args)
        {
            //new in Method enables to create new methods with the names of existing sealed base mathods
            //new in Method will run the base method if not cast..(method hidding)
            ProductA P = new ProductC();
            ((ProductC)P).SayMyName();
            P.OverideTest();

            //Factory Class Example
            BaseFactory newFactory = new FactoryClass();
            Client myClient = new Client(newFactory);
            myClient.sayClientProductNameA();
            myClient.sayClientProductNameB();

            // Factory Methods - if you have alot of constructor overloads// 
            // Instead of overloading constructors of a class to return diffrent objects 
            // we create public static methods that return new object instantiation.
            // so we call: var objectType = ClassName.ObjectTypeMethod();

            //Builder - a Class Uses Build method that receives a specific builder object
            //and Uses the builder to return a object instance by running builder methods to create the object .
            ProductStore store = ProductStore.Instance;
            var myProduct = store.Build(new ProductBBuilder());
            myProduct.SayMyName();

            //Prototype pattern
            // uses IClonable interface and Clone() method to make a deep clone
            // adds cloning to reference members by creating new ref objects or calling the ref obj clone method .
            // MemberwiseClone() creates a shallow copy (only copies value members)...
            // so ref member are only refernced and not copied they only point to same ref.  

            //Stratergy pattern
            // uses Class instences of an interface as a variable in another class
            // interface fly{}
            // class flying : fly{
            // }
            // class notflying : fly{
            // }
            // class animal{
            //  public fly isFlying;
            //  isFlying = new ..; 
            // }
        

            //Singleton
            //look implementation in ProductStore.cs


            //Delegates
            //can be outside class definition (is its own Class)
            //can be passed to functions
            //can be assigned by =

            //public delegate int delegateName(arg1,arg2..);

            //delegateName delVar,delVar2;

            //To link event Handlers:   delVar += methodName;  OR  delVar2 += new delegateName(methodName);
            //If more then One link to delegate the return value will be of the last method.
            //Lambdas (,)=> logic ;
            //delegate(){  annonymos function     };

            //Special Delegates:
            //Action<ArgsTypes,..> : returns void gets ArgsTypes
            //Func<ArgsTypes,..,ReturnType> : returns ReturnType gets ArgsTypes
            //Prediacte<ArgsTypes,..> :returns Bool gets ArgsTypes
            //EventHandler<CustomEventArgs> eventName; creates an annonymous delegate with void (object sender,CustomEventArgs e)

            //Events
            //must be inside class def
            //public event delegateName eventName;
            //To link event Handlers:   eventName += methodName;  OR  eventName += new delegate ...(methodName);
            //should be called from methods named "OnEventName"
            //check eventName!=null before running eventName();

            myDelegate delVar;
            //linking to Delegate/Event Handler Method
            delVar = delHandler;
            //invoking the Delegate
            var result = OnMyDelegate(1, 2, delVar);
            Console.WriteLine("The Result Is:{0}", result);


            //Tasks Async,Parallel
            //--Only Main Thread Can Update UI--
            //TaskScheduler.FromCurrentSynchronizationContext() - a parameter to run a Task on Main Thread (UI Thread)

            Task T = new Task(delegate { Console.WriteLine("Task"); });
            //T2 starts after T Completes 
            Task T2 = T.ContinueWith((perviousTask) => { Console.WriteLine("Task2"); });
            T.Start();

            //The Better Way To Run Tasks (Create and Runs Task)
            Task T3 = Task.Factory.StartNew(() => Console.WriteLine("Task3"));

            //T4 Waits for T1,T2,T3 completion
            Task T4 = Task.Factory.ContinueWhenAll(new Task[] { T, T2, T3 }, (tasks) => Console.WriteLine("Task4"));
            //T5 Waits for any of T1,T2,T3 completion
            Task T5 = Task.Factory.ContinueWhenAny(new Task[] { T, T2, T3 }, (tasks) => Console.WriteLine("Task5"));

            int a = 10;
            Task T6 = Task.Factory.StartNew(() => a += 10);
            //Waiting to T6 to finish before printing a
            T6.Wait();
            //Task status : RanToCompletion,Canceled,Faulted
            Console.WriteLine("T6 Status is:{0}", T6.Status);
            Console.WriteLine("a value is:{0}", a);
            
            //Returning Result from a Task 
            Task<int> T7 = Task.Factory.StartNew(() =>
            {
                int taskVar = 100;
                return taskVar += 10;
            });

            //Waits for Result from a Task and gets return value
            Console.WriteLine("T7 Result is : {0}", T7.Result);
            Thread.Sleep(100);

            //returns the index of first task completed
            int index = Task.WaitAny(new Task[] { T, T2, T3 });
            Console.WriteLine("index of first Task completed : {0}", index);

            //WaitAllOneByOne
            //Process the results by the order they finish
            List<Task<int>> listOfTasks = new List<Task<int>>();

            int n = 3;
            for (int i = 0; i < n; i++)
            {
                //copy fixes clousre of i variable so each lambda receives diffrent value
                int copy = i;
                listOfTasks.Add(Task.Factory.StartNew<int>(() => { return copy; }));
            }

            while (listOfTasks.Count > 0)
            {
                int arrIndex = Task.WaitAny(listOfTasks.ToArray());
                Console.WriteLine("finished with Result {0}", listOfTasks[arrIndex].Result);
                listOfTasks.RemoveAt(arrIndex);
            }

            //Exception Handling in Tasks
            Task<int> T8 = Task.Factory.StartNew<int>(() => throw new Exception("Task Exception"));
            try
            {
                //AggregateException is thrown in Result,Wait,WaitAll,Exception(Property of Task) Can Be A Tree Of Exceptions
                //WaitAny Does NOT throw an Exception
                Console.WriteLine(T8.Result);
            }
            catch (AggregateException ae)
            {
                //Flattens Exceptions Tree
                ae = ae.Flatten();
                foreach (Exception ex in ae.InnerExceptions)
                {
                    //The Task Exception is inside ae.InnerException
                    Console.WriteLine(ae.InnerException.Message);
                }
            }

            //status is Faulted because of Exception
            Console.WriteLine(T8.Status);

            //Last resort for Handeling Tasks Exceptions 
            //Register to TaskScheduler.UnobservedTaskException Event -- Should be Done Once pref in static Ctor --
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            //Task Cancellation
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken cancelToken = cts.Token;

            //A Way to send method to Task.Factory.StartNew by running function inside lambda
            //send Cancellation Token to Task and as parameter to function
            // return value from lambda Expression to task
            Task<int> T9 = Task.Factory.StartNew<int>(() => { return taskCancellationExample(cancelToken, 1, 1); }, cancelToken);
            Thread.Sleep(2000);

            //Throw Cancel Request
            cts.Cancel();

            //after Cancel you need to create new CancellationTokenSource so they can use it again
            cts = new CancellationTokenSource();

            try
            {
                //the Cancellation Exception is thrown here in T9.Result
                Console.WriteLine("Task T9 Returned:{0}", T9.Result);
            }
            catch (AggregateException ae)
            {
                var exceptions = ae.Flatten();
                foreach (var e in exceptions.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //Parent-Child Tasks : The Parent Task Doesn't complete until all child tasks completed
            Task parent = Task.Factory.StartNew(() =>
            {
                Task child1 = Task.Factory.StartNew(() => { Console.WriteLine("child1"); }, TaskCreationOptions.AttachedToParent);
                Task child2 = Task.Factory.StartNew(() => { Console.WriteLine("child2"); }, TaskCreationOptions.AttachedToParent);
            });

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                var exceptions = ae.Flatten();
                foreach (var e in exceptions.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //Passing Data In Safe Way To Lambda (prevent Clousre problem)
            string[] strings = { "test", "test2", "test3" };
            foreach (string str in strings)
            {
                //sending the str though arg parameter
                Task t = Task.Factory.StartNew((arg) => { Console.WriteLine(arg); }, str);
            }

            //To Make Output To Console Restricted to Each Thread use lock(Console.Out)

            //Before Application Closed Should Cancel Running Tasks

            T.ContinueWith(t =>
            {
                switch (t.Status)
                {
                    case TaskStatus.Canceled:
                              // Do somthing
                        break;
                    case TaskStatus.Faulted:
                             // Do somthing
                        break;
                    case TaskStatus.RanToCompletion:
                        // Do somthing
                        break;
                    default:
                        break;
                }

            });

            //Creating a Random Number Array of any size
            Randomizer r = new Randomizer(10);

            //TaskCompletionSource is facade that can be used to make IAsyncResult "old" code to Task
            //TaskCompletionSource<TResult> tc = new TaskCompletionSource<TResult>(iar);
            //Task t = tc.Task;
            //Results can be set by tc.setResult(..)
            Console.WriteLine("Hello World!");
        }



        private static int taskCancellationExample(CancellationToken cancelToken, int a, int b)
        {
            while (a < 100)
            {
                //Checks if Cancel Requested
                if (cancelToken.IsCancellationRequested)
                {
                    //Do CleanUp
                    //Throws Cancellation Exception
                    cancelToken.ThrowIfCancellationRequested();
                }
                a += b;
                Thread.Sleep(200);
                Console.WriteLine("a value is:{0}", a);
            }
            return a;
        }
        //Method for Handling UnobservedTaskExceptions
        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine("**UnObserved:" + e.Exception.Message);
            e.SetObserved();
        }


        //Delegate/Event Handler Method
        static public int delHandler(int a, int b)
        {
            return a + b;
        }

        //Raising The Delegate/Event
        static public int? OnMyDelegate(int a, int b, myDelegate delVar)
        {
            if (delVar != null)
            {
                return delVar(a, b);
            }
            else
            {
                Console.WriteLine("Delegate null");
                return null;
            }
        }

    }
}
