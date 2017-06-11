using System;
using NUnit.Framework;

namespace XTAF.Core
{
    [Parallelizable]
    public class CoreTest<T>
    {
        private static CoreDriver<T> coreDriver = null;

        public T driver => coreDriver.driver;

        public TestContext currentTestContext;
        BrowserType browserType;

        public CoreTest(BrowserType browserType)
        {
            this.browserType = browserType;
            coreDriver = coreDriver?? CoreDriver<T>.Init();
        }

        [SetUp]
        public void TestSetup()
        {
            currentTestContext = TestContext.CurrentContext;

            // Set description from property.
            string TestDescription = "[No Description]";
            foreach (string descriptionKeyword in new[] { "Desc", "Description" })
                if (currentTestContext.Test.Properties[descriptionKeyword].Count != 0)
                    TestDescription = currentTestContext.Test.Properties[descriptionKeyword][0].ToString();

            PreSetupHook(currentTestContext);

            // driver maybe reset from previous test.
            if (driver == null)
                StartDriver(browserType);
                        
            PostSetupHook();
        }

        /// <summary>
        /// Calls pre teardown method first to handle safe quitting of driver followed by disposing the driver reference from the threadmap
        /// </summary>
        [TearDown]
        public void TestTearDown()
        {
            TestPreTearDownHook();
            coreDriver.DisposeDriver();            
        }        

        /// <summary>
        /// Initializes the WebDriver for current test thread and adds it to the driver thread map and returns the driver instance
        /// This way separate instance for driver would be created and maintained for each test thread and hence, isloated driver
        /// will be created for each test during parallel execution.
        /// </summary>
        /// <param name="browserType"></param>
        /// <returns></returns>
        private void StartDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Firefox:
                    coreDriver.AddDriver(LaunchFirefox());
                    break;
                case BrowserType.Chrome:
                    coreDriver.AddDriver(LaunchChrome());
                    break;
                case BrowserType.Edge:
                    coreDriver.AddDriver(LaunchFirefox());
                    break;
                case BrowserType.IE:
                    coreDriver.AddDriver(LaunchFirefox());
                    break;
                case BrowserType.Safari:
                    coreDriver.AddDriver(LaunchSafari());
                    break;
            }
        }        

        public virtual void TestPreTearDownHook() { }
        public virtual void PreSetupHook(TestContext testContext) { }
        public virtual void PostSetupHook() { }
        public virtual T LaunchFirefox() { throw new NotImplementedException("Please override this method on BaseTest"); }
        public virtual T LaunchChrome() { throw new NotImplementedException("Please override this method on BaseTest"); }
        public virtual T LaunchEdge() { throw new NotImplementedException("Please override this method on BaseTest"); }
        public virtual T LaunchIE() { throw new NotImplementedException("Please override this method on BaseTest"); }
        public virtual T LaunchSafari() { throw new NotImplementedException("Please override this method on BaseTest"); }
    }
}
