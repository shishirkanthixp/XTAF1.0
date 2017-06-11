namespace XTAF.Core
{
    /// <summary>
    /// This class inherits from Driver class which holds a driver property that resolves the instance of WebDriver
    /// from threadmap (a dictionary holding threadID against the webdriver instances)
    /// </summary>
    public class CorePage<T>
    {
        public CoreDriver<T> coreDriver;
        public T driver => coreDriver.driver;
        public CorePage()
        {
            coreDriver = coreDriver ?? CoreDriver<T>.Init();            
        }
    }
}
