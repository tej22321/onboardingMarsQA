using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA.Drivers
{
    public class CommonDriver
    {
        public static IWebDriver driver;

        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    throw new NullReferenceException("WebDriver instance was not initialized. You should call InitializeDriver method first.");
                }
                return driver;
            }
        }

        public static void InitializeDriver(string browser)
        {
            if (driver == null)
            {
                switch (browser.ToLower())
                {
                    case "chrome":
                        driver = new ChromeDriver();
                        break;
                    case "firefox":
                        driver = new FirefoxDriver();
                        break;
                    default:
                        throw new ArgumentException($"Browser '{browser}' is not supported.");
                }
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Manage().Window.Maximize();
            }
        }

        public static void CloseDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

    }
}
