using System;
using System.Linq;
using System.IO;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using XTAF.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace XTAF.Test
{
    [TestFixture(BrowserType.Firefox)]
    [TestFixture(BrowserType.Chrome)]
    [TestFixture(BrowserType.Edge)]
    [TestFixture(BrowserType.IE)]
    public class BaseTest:CoreTest<IWebDriver>
    {
        public BaseTest(BrowserType browserType) : base(browserType) { }

        TimeSpan driverCommandTimeout = new TimeSpan(0, 1, 0);

        public override void PostSetupHook()
        {
            driver.Url = "https://google.co.in";
        }

        public override IWebDriver LaunchFirefox()
        {
            return new FirefoxDriver();
        }

        public override IWebDriver LaunchChrome()
        {
            int indexofProjectFolder = currentTestContext.WorkDirectory.LastIndexOf("XTAF1.0");
            string packagesPath = Path.Combine(currentTestContext.WorkDirectory.Substring(0, indexofProjectFolder)+"Packages");
            string chromeDirName = Directory.GetDirectories(packagesPath)
                        .ToList()
                        .LastOrDefault(dirName => dirName.Contains("Selenium.WebDriver.ChromeDriver"));
            string chromeDriverPath = Path.Combine(chromeDirName, "driver", "win32");

            return new ChromeDriver(chromeDriverPath, ChromeOptionsHook(), driverCommandTimeout);
        }

        private ChromeOptions ChromeOptionsHook()
        {
            var toReturn = new ChromeOptions();
            return toReturn;
        }

        public override void TestPreTearDownHook()
        {
            driver.Quit();            
        }
    }
}
