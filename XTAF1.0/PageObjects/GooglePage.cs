using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace XTAF.PageObjects
{
    class GooglePage:BasePage
    {

        public IWebElement txtSearchBox => driver.FindElements(By.CssSelector("#lst-ib")).FirstOrDefault();
        public IWebElement btnSearh => driver.FindElements(By.CssSelector("[name='btnK'],.lsb,[name='btnG']")).FirstOrDefault(btn => btn.Displayed);
        public List<IWebElement> resultsList => driver.FindElements(By.CssSelector("h3")).ToList();


        //public GooglePage(IWebDriver driver)
        //{

        //    base.driver = driver;
        //    PageFactory.InitElements(base.driver, this);
        //}
        //[FindsBy(How =How.CssSelector , Using = "#lst-ib")]
        //public IWebElement txtSearchBox { get; set; }


        //[FindsBy(How = How.CssSelector, Using = "[name='btnK'],.lsb")]
        //public IWebElement btnSearh { get; set; }



        //[FindsBy(How = How.CssSelector, Using = "h3")]
        //public IList<IWebElement> resultsList { get; set; }

    }
}
