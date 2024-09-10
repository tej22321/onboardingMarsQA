using MarsQA.Drivers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.SpecflowPages
{
    public class HomePage
    {
        private IWebDriver driver;

        public HomePage()
        {
            this.driver = CommonDriver.driver;
        }
        public void GoToProfileTab()
        {
            try
            {
                driver.FindElement(By.XPath("//a[text()='Profile']")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("element not found" + e.Message);
            }
        }

    }
}
