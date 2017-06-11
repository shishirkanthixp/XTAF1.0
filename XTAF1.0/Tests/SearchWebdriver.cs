using NUnit.Framework;
using XTAF.PageObjects;
using System;
using System.Linq;
using XTAF.Core;

namespace XTAF.Test
{
    public class SearchWebdriver :BaseTest
    {
        public SearchWebdriver(BrowserType browserType) : base(browserType)
        {
        }

        [Test]
        public void searchWebdriver()
        {

            var ge = new GooglePage();
            
            ge.txtSearchBox.SendKeys("webdriver");

            ge.btnSearh.Click();

            System.Threading.Thread.Sleep(3000);
            //ge.wait.Until(driver => ge.resultsList.Count > 0);

            int count = ge.resultsList.Count();

            for(int i=0;i<count;i++)
            {
                Console.WriteLine(ge.resultsList[i].Text.ToString());
                if(ge.resultsList[i].Text!="")
                Assert.IsTrue(ge.resultsList[i].Text.ToLower().Contains("webdriver"), "the search is "+ ge.resultsList[i].Text);
            }
        }
    }
}
