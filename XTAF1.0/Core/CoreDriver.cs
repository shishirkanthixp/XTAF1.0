using System;
using NUnit.Framework;
using System.Threading;
using System.Collections.Concurrent;

namespace XTAF.Core
{    
    public class CoreDriver<T>:ICoreDriver
    {
        private CoreDriver() { }

        private static CoreDriver<T> coreDriver = null;
        public static CoreDriver<T> Init()
        {
            coreDriver = coreDriver ?? new CoreDriver<T>();
            return coreDriver;            
        }

        private static ConcurrentDictionary<int, T> driverThreadMap = new ConcurrentDictionary<int, T>();
        static string packagesPath = String.Empty;
        
        //Static constructor to initialize the static vaiables of the class
        //Note: static constructor does not have a access modifier (public)
        static CoreDriver()
        {
            packagesPath = TestContext.CurrentContext.TestDirectory
                .Remove(TestContext.CurrentContext.TestDirectory.IndexOf("XTAF1.0\\bin")) + "\\packages";            
        }

        /// <summary>
        /// Property which verifies if webdriver instance exists for current thread ID and returns same if existed
        /// </summary>
        public T driver
        {
            get
            {
                T currentThreadDriver = default(T);
                if (driverThreadMap.TryGetValue(Thread.CurrentThread.ManagedThreadId, out currentThreadDriver))
                    return currentThreadDriver;
                else
                    return default(T);
            }
            set
            {
                // remove existing if present.
                T placeholder = default(T);
                driverThreadMap.TryRemove(Thread.CurrentThread.ManagedThreadId, out placeholder);
                // add
                driverThreadMap.TryAdd(Thread.CurrentThread.ManagedThreadId, value);
            }            
        }

        public void AddDriver(T driverInstance)
        {
            driver = driverInstance;
        }

        public void DisposeDriver()
        {
            T dummyOut;
            driver = default(T);
            driverThreadMap.TryRemove(Thread.CurrentThread.ManagedThreadId, out dummyOut);
        }
    }
}
