using NUnit.Framework;
using XTAF.Core;
using XTAF.PageObjects;

namespace XTAF.Test
{
    public class TestClass1:BaseTest
    {
        /// <summary>
        /// Each class has to implement this constructor to inherit the base test
        /// </summary>
        /// <param name="browserType"></param>
        public TestClass1(BrowserType browserType) : base(browserType) { }

        [Test]        
        public void SearchWebSelenium()
        {
            GooglePage gPO = new GooglePage();
            gPO.txtSearchBox.SendKeys("selenium");
            System.Threading.Thread.Sleep(2000);
            gPO.btnSearh.Click();
            System.Threading.Thread.Sleep(2000);
            int resultCount = gPO.resultsList.Count;
            for (int i = 0; i<resultCount; i++)
            {
                TestContext.WriteLine(gPO.resultsList[i].Text);
                Assert.IsTrue(gPO.resultsList[i].Text.ToLower().Contains("selenium")
                    , "Expected: selenium; Actual: " + gPO.resultsList[i].Text);
            }
        }
    }
}
