using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using MarsQA.Drivers;
using System.Collections.ObjectModel;

namespace MarsQA.SpecflowPages
{
    public class LanguagesPage
    {
        private IWebDriver driver;

        public LanguagesPage()
        {
            this.driver = CommonDriver.driver;
        }

        public void CreateLanguageRecord()
        {
            try
            {
                //click on add new button for language
                driver.FindElement(By.XPath("//div[@data-tab='first']//div[text()='Add New']")).Click();
                
                //find the language column and add language Telugu
                driver.FindElement(By.XPath("//input[@name='name']")).SendKeys("Telugu");


                SelectElement drpLevel = new SelectElement(driver.FindElement(By.Name("level")));
                drpLevel.SelectByText("Fluent");

                driver.FindElement(By.XPath("//input[@value= 'Add']")).Click();
            }
            catch (Exception e)
            {
                Console.WriteLine("Create Language error" + e.Message);


            }


        }
        public String VerifyLanguageRecordCreated(String Language)
        {

            IWebElement LanguageElement = driver.FindElement(By.XPath("//tr/td[text() = 'Telugu']"));
            string LanguageText = LanguageElement.Text;
            return LanguageText;
          
        }

        public void CleanupLanguageRecords()
        {
            By tbodyLocator = By.XPath("//div[@data-tab = 'first']//tbody");


            if (IsElementPresent(driver, tbodyLocator))
            {
                // Find  the tbody element
                ReadOnlyCollection<IWebElement> tbodyElement = driver.FindElements(tbodyLocator);


                foreach (IWebElement tbody in tbodyElement)
                {
                    
                    //remove the language row entered
                    tbody.FindElement(By.XPath("//div[@data-tab = 'first']//tbody/tr/td[3]/span/i[@class = 'remove icon']")).Click();

                    Console.WriteLine("The tbody element(Languages rows) were found and deleted.");
                }
            }
            else
            {
                Console.WriteLine("The tbody element (languages rows) were not found.");
            }

            static bool IsElementPresent(IWebDriver driver,By locator)
            {
                try
                {
                    driver.FindElement(locator);
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }


            }

        }
}
