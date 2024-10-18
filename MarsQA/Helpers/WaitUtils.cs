﻿using MarsQA.Drivers;
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

        public WaitUtils(CommonDriver commonDriver)
        {
            this.driver = commonDriver.Driver;
        }

        // Explicit wait for element to be visible

        public IWebElement waitForElementToBeVisible(By locator)
        {

            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(60));
            return wait.Until(ExpectedConditions.ElementExists(locator));
        }

        public bool waitForElementToBeInvisible(By locator)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
           return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public bool waitForElementToBeStale(IWebElement locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            return wait.Until(ExpectedConditions.StalenessOf(locator));
        }


    }
}
