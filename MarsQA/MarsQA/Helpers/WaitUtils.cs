using MarsQA.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.Helpers
{
    
    public class WaitUtils
    {
        private IWebDriver driver;

        public WaitUtils()
        {
            this.driver = CommonDriver.driver;
        }

        // Explicit wait for element to be visible
        public IWebElement waitForElementToBeVisible(By locator, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(60));
            return wait.Until(ExpectedConditions.ElementExists(By.Id("id")));
        }


    }
}
