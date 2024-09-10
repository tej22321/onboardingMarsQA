using MarsQA.Configuration;
using MarsQA.Drivers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.SpecflowPages
{
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage() {
            this.driver = CommonDriver.driver;
        }
        public void LoginSteps()
        {
            try
            {
               
                driver.Navigate().GoToUrl(Config.Url);
                Thread.Sleep(1500);

                driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]")).Click();
                driver.FindElement(By.XPath("//input[@name = 'email']")).SendKeys(Config.Username);
                driver.FindElement(By.XPath("//input[@name = 'password']")).SendKeys(Config.Password);
                driver.FindElement(By.XPath("//button[contains(text(),'Login')]")).Click();
            }

            catch (NoSuchElementException e)
            {
                Console.WriteLine("Element not found: " + e.Message);
            }
            catch (WebDriverException e)
            {
                Console.WriteLine("WebDriver error: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("General error: " + e.Message);
            }
        }

    }
}
