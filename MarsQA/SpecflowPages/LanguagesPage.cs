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
using OpenQA.Selenium.DevTools.V124.Runtime;
using NUnit.Framework.Interfaces;
using System.Security.Cryptography.X509Certificates;
using MarsQA.Helpers;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.CommonModels;
using OpenQA.Selenium.DevTools.V124.Debugger;
using TechTalk.SpecFlow.Configuration.JsonConfig;

namespace MarsQA.SpecflowPages
{
    public class LanguagesPage
    {
        private IWebDriver driver;

        private WaitUtils waitUtils;

        public LanguagesPage(CommonDriver commonDriver)
        {
            this.driver = commonDriver.Driver;
            this.waitUtils = new WaitUtils(commonDriver);
        }

        private IWebElement AddNewElement  => driver.FindElement(By.XPath("//div[@data-tab='first']//div[text()='Add New']"));
        private IWebElement LangugageElement => driver.FindElement(By.XPath("//input[@name='name']"));
        private IWebElement LevelElement => driver.FindElement(By.Name("level"));
        private IWebElement AddELement => driver.FindElement(By.XPath("//input[@value= 'Add']"));

        private By tbodyLocator = By.XPath("//div[@data-tab = 'first']//tbody");

        private IWebElement SignoutElement => driver.FindElement(By.XPath("//button[contains(text(),'Sign Out')]"));
        private IWebElement removeElement => driver.FindElement(By.XPath("//div[@data-tab = 'first']//tbody/tr/td[3]/span/i[@class = 'remove icon']"));

        private IWebElement LanguageTab => driver.FindElement(By.XPath("//a[contains(text(),'Languages')]"));


        private String LanguageElementValue = null;

        private String LanguageLevelValue = null;

        private IWebElement languagetableElement => driver.FindElement(By.XPath($"//td[contains(text(),'{LanguageElementValue}')]"));

        private IWebElement LanguageRow => driver.FindElement(By.XPath($"//tbody/tr[td[text() ='{LanguageElementValue}']]/td[contains(text(),'{LanguageLevelValue}')]"));
        private IWebElement editElement => driver.FindElement(By.XPath($"//tbody/tr[td[text() = '{LanguageElementValue}'] and td[text() = '{LanguageLevelValue}']]/td/span/i[contains(@class, 'write')]"));

        private IWebElement editLanguageElement => driver.FindElement(By.XPath($"//input[@value = '{LanguageElementValue}']"));

        private IWebElement updateLanguageButton => driver.FindElement(By.XPath("//input[@value = 'Update']"));
        
        private IWebElement ErrorMessage => driver.FindElement(By.XPath("//div[contains(@class,'ns-type-error')]/div[contains(@class,'ns-box-inner')]"));

        private IWebElement SuccessMessage => driver.FindElement(By.XPath("//div[contains(@class,'ns-type-success')]/div[contains(@class,'ns-box-inner')]"));
        
        
        private String ErrorMessageText = null;

        private List<object> languageElementsValues = new List<object>();

        

        private ReadOnlyCollection<IWebElement> tableRows => driver.FindElements(By.XPath("//tbody/tr"));


        public void NavigateToLanguageTab()
        {
            LanguageTab.Click();
        }

        public void ClickAddNew()
        {
            AddNewElement.Click();
        }

        public void EnterLangugage(String language)
        {
            LangugageElement.SendKeys(language);
            LanguageElementValue = LangugageElement.GetAttribute("value");

        }

        public void SelectLevel(String level)
        {

            SelectElement drpLevel = new SelectElement(LevelElement);
            drpLevel.SelectByText(level);
            LanguageLevelValue = drpLevel.SelectedOption.Text;
            
        }

        public void ClickAdd()
        {
            AddELement.Click();
           languageElementsValues.Add(LanguageElementValue);
            
            
            try
            {
                if ((ErrorMessage.Displayed))
                {
                    ErrorMessageText = ErrorMessage.Text;
                    Console.WriteLine(ErrorMessageText);
                }
              
            }
            catch(NoSuchElementException e) {
                Console.WriteLine("no Error messsage was found while addding the language record");
            }
        }

 

        public void AddLanguageRecord(String language, String level, int rowcount)
        {
            if(rowcount > 4)
            {
                try
                {
                    driver.FindElement(By.XPath("//thead/tr[th[text() = 'Language']]/th/div[text() = 'Add New']"));
                }catch(Exception e) {

                    Console.WriteLine("No access to add more than 4 record");
                    return;
                }
            }
            try
            {
                ClickAddNew();

                EnterLangugage(language);

                SelectLevel(level);

                ClickAdd();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        public ReadOnlyCollection<IWebElement> GetTableRows()
        {
            return driver.FindElements(By.XPath("//tbody/tr"));
        }

        public bool VerifyLanguageRecordCreated()
        {
            try
            {
                                       
                // for language elemnt which has numeric data
                int result;
                if (int.TryParse(LanguageElementValue, out result) || Regex.IsMatch(LanguageElementValue, @"[^a-zA-Z0-9\s]") || LanguageElementValue.Trim().Equals(""))
                {
                    if (LanguageRow != null && LanguageRow.Displayed)
                    {
                       // languageElementsValues.Add(LanguageElementValue);
                        Console.WriteLine("invalid language added to the Language table");
                        if (removeElement != null)
                        {
                            removeElement.Click();
                        }
                        return false;

                    }
                    else
                    {
                        return true;
                    }

                }
                else if (LanguageRow.Displayed)
                {
                    languageElementsValues.Add(LanguageElementValue);
                    removeElement.Click();
                    return true;
                }
                else return false;
            }

            catch (NoSuchElementException)
            {
                if (ErrorMessageText != null)
                {
                    return true;

                }
                Console.WriteLine("here in no such element");
                return false;
                
            }
            catch (Exception e)
            {


                Console.WriteLine("language record not found with language" + LanguageElementValue + "," + LanguageLevelValue);
                return false;

            }
           
        }
        public void CleanupLanguageRecords()
        {

            try
            {

                if (IsElementPresent(driver, tbodyLocator))
                {
                    ReadOnlyCollection<IWebElement> tableRows =  GetTableRows();
                    while (tableRows.Count > 0)
                    {
                        
                        
                        foreach (IWebElement tableRow in tableRows)
                        {
                            
                           IWebElement removeElement = tableRow.FindElement(By.XPath("./td[3]/span/i[@class = 'remove icon']"));
                           removeElement.Click();
                            waitUtils.waitForElementToBeInvisible(By.XPath("//div[contains(@class,'ns-type-success')]/div[contains(@class,'ns-box-inner')]"));                  
                            // Re-fetch the tableRows after removing an element
                             tableRows = GetTableRows();
                                                     
                           // Break out of the foreach loop to restart from the first row, since the DOM has been updated
                            break;
                        }

                       
                    }


                }
                else
                {
                    Console.WriteLine("No records found.");
                }

                static bool IsElementPresent(IWebDriver driver, By locator)
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
            }catch (Exception e) { Console.WriteLine(e.ToString()); }

        }


    
  public void DeleteLanguageEntry()
        {

            try
            {
                if (languagetableElement.Displayed)
                {


                    Console.WriteLine(languageElementsValues.Count + "here in deletentry in the if statement");


                    foreach (object languageElement in languageElementsValues)
                    {
                        LanguageElementValue = languageElement.ToString();

                        Console.WriteLine(LanguageElementValue + " here in deleteentry");
                        Console.WriteLine(languageElementsValues.Count + LanguageElementValue);
                        removeElement.Click();
                    }

                }
                else
                {
                    Console.WriteLine("No record found to delete");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("No such record found to delete");
            }
        }

        public bool IsErrorMessageExists()
        {
            try
            {
                if (ErrorMessageText != null)
                {
                    CleanupLanguageRecords();
                    return true;
                }
                CleanupLanguageRecords();
                return false;

            }
            catch (Exception e) {
         
                    Console.WriteLine(e.ToString());    
                    return false; 
            
            }
        }


       
       public void UpdateLanguageRecord(String language,String level,String updateLanguage)
        {
            
            this.LanguageElementValue = language;

            this.LanguageLevelValue = level;
            //click on edit icon
            editElement.Click();

            // Find language textbox, clear the text and edit the field
            editLanguageElement.Clear();
            editLanguageElement.SendKeys(updateLanguage);

            this.LanguageElementValue = updateLanguage;
            //click on update button
            updateLanguageButton.Click();
        }


        public bool VerifyUpdatedLanguageRecord()
        {
            try
            {
                if (LanguageRow.Displayed)
                {
                    removeElement.Click();
                    return true;
                }

                else
                {
                    return false;
                }
            }catch (Exception e)
            {
                Console.WriteLine(e.ToString());    
                return false;
            }

        }

        public void removeLanguageRecord(String language,String level)
        {
            try
            {
                this.LanguageElementValue = language; this.LanguageLevelValue = level;
                if (LanguageRow.Displayed)
                {
                    
                    removeElement.Click();

                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool VerifyLanguageRecordRemoved()
        {
            try
            {
                 By tablexpath = By.XPath($"//tbody/tr[td[text() ='{LanguageElementValue}']]/td[contains(text(),'{LanguageLevelValue}')]");

                               
                if (waitUtils.waitForElementToBeInvisible(tablexpath))
                {
                    return true;
                }
                
                return false;
            }catch (Exception e) { 
              Console.WriteLine(e.ToString());
                return false;
            }
        }
        public bool VerifyMaxLimitOfRecords(int limit)
        {
            try
            {
                
                if (tableRows.Count > limit)
                {
                    CleanupLanguageRecords();
                     return false;
                }
                CleanupLanguageRecords();
                return true;
                
            }
            catch (Exception e)
            {
                CleanupLanguageRecords();
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
