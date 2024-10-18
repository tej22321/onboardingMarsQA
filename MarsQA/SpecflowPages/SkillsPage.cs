using MarsQA.Drivers;
using MarsQA.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Configuration.JsonConfig;

namespace MarsQA.SpecflowPages
{
    public class SkillsPage
    {
        private IWebDriver driver;

        private WaitUtils waitUtils;

        public SkillsPage(CommonDriver commonDriver) {
            this.driver = commonDriver.Driver;
            this.waitUtils = new WaitUtils(commonDriver);
            }

        private IWebElement SkillsTab => driver.FindElement(By.XPath("//a[contains(text(),'Skills')]"));

        private IWebElement AddNewElement => driver.FindElement(By.XPath("//div[@data-tab='second']//div[text()='Add New']"));

        private By tbodyLocator = By.XPath("//div[@data-tab = 'second']//tbody");
        private IWebElement LevelElement => driver.FindElement(By.Name("level"));

        private IWebElement SkillElement => driver.FindElement(By.XPath("//input[@name='name']"));

        private String SkillElementValue = null;

        private String SkillLevelValue = null;

        private IWebElement AddELement => driver.FindElement(By.XPath("//input[@value= 'Add']"));

        private List<object> SkillsElementValues = new List<object>();


        private IWebElement ErrorMessage => driver.FindElement(By.XPath("//div[contains(@class,'ns-type-error')]/div[contains(@class,'ns-box-inner')]"));

        private String ErrorMessageText = null;


        private IWebElement SkillRow => driver.FindElement(By.XPath($"//tbody/tr[td[text() ='{SkillElementValue}']]/td[contains(text(),'{SkillLevelValue}')]"));

        private IWebElement removeElement => driver.FindElement(By.XPath("//div[@data-tab = 'second']//tbody/tr/td[3]/span/i[@class = 'remove icon']"));

        private IWebElement editElement => driver.FindElement(By.XPath($"//tbody/tr[td[text() = '{SkillElementValue}'] and td[text() = '{SkillLevelValue}']]/td/span/i[contains(@class, 'write')]"));

        private IWebElement editSkillElement => driver.FindElement(By.XPath($"//input[@value = '{SkillElementValue}']"));
        private IWebElement updateSkillButton => driver.FindElement(By.XPath("//input[@value = 'Update']"));

        private ReadOnlyCollection<IWebElement> tableRows => driver.FindElements(By.XPath("//tbody/tr"));
        public void NavigateToSkillsTab()
        {
            SkillsTab.Click();
        }

        public void ClickAddNew()
        {
            AddNewElement.Click();
        }

        public void EnterSkill(String skill)
        {
            SkillElement.SendKeys(skill);
            SkillElementValue = SkillElement.GetAttribute("value");

        }
        public ReadOnlyCollection<IWebElement> GetTableRows()
        {
            return driver.FindElements(By.XPath("//tbody/tr"));
        }

        public void SelectLevel(String level)
        {

            SelectElement drpLevel = new SelectElement(LevelElement);
            drpLevel.SelectByText(level);
            SkillLevelValue = drpLevel.SelectedOption.Text;

        }


        public void ClickAdd()
        {
            AddELement.Click();
            SkillsElementValues.Add(SkillElementValue);


            try
            {
                if ((ErrorMessage.Displayed))
                {
                    ErrorMessageText = ErrorMessage.Text;
                    Console.WriteLine(ErrorMessageText);
                    //languageElementsValues.Remove(LanguageElementValue);
                }

            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine("no Error messsage was found while addding the language record");
            }
        }

        public void CleanupSkillsRecords()
        {

            try
            {

                if (IsElementPresent(driver, tbodyLocator))
                {
                    ReadOnlyCollection<IWebElement> tableRows = GetTableRows();
                    while (tableRows.Count > 0)
                    {


                        foreach (IWebElement tableRow in tableRows)
                        {
                            waitUtils.waitForElementToBeVisible(By.XPath("./td[3]/span/i[@class = 'remove icon']"));
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
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }

        }
        public bool VerifySkillRecordCreated()
        {
            try
            {
                Console.WriteLine(SkillElementValue);
                // for language elemnt which has numeric data
                int result;
                if (int.TryParse(SkillElementValue, out result) || Regex.IsMatch(SkillElementValue, @"[^a-zA-Z0-9\s]") || SkillElementValue.Trim().Equals(""))
                {
                    if (SkillRow != null && SkillRow.Displayed)
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
                else if (SkillRow.Displayed)
                {
                    SkillsElementValues.Add(SkillElementValue);
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


                Console.WriteLine("Skill record not found with Skill" + SkillElementValue + "," + SkillLevelValue);
                return false;

            }

        }

        public bool IsErrorMessageExists()
        {
            try
            {
                Console.WriteLine(ErrorMessageText );
                if (ErrorMessageText != null)
                {
                    CleanupSkillsRecords();
                    return true;
                }
                CleanupSkillsRecords();
                return false;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                return false;

            }
        }


        public void UpdateSkillRecord(String skill, String level, String updateSkill)
        {

            this.SkillElementValue = skill;

            this.SkillElementValue = skill;
            //click on edit icon
            editElement.Click();

            // Find language textbox, clear the text and edit the field
            editSkillElement.Clear();
            editSkillElement.SendKeys(updateSkill);

            this.SkillElementValue = updateSkill;
            //click on update button
            updateSkillButton.Click(); 
        }

        public bool VerifyUpdatedSkillRecord()
        {
            try
            {
                if (SkillRow.Displayed)
                {
                    removeElement.Click();
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }
        public void removeSkillRecord(String skill, String level)
        {
            try
            {
                this.SkillElementValue = skill; this.SkillLevelValue = level;
                if (SkillRow.Displayed)
                {

                    removeElement.Click();


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool VerifySkillRecordRemoved()
        {
            try
            {
                By tablexpath = By.XPath($"//tbody/tr[td[text() ='{SkillElementValue}']]/td[contains(text(),'{SkillLevelValue}')]");


                if (waitUtils.waitForElementToBeInvisible(tablexpath))
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public void AddSkillsRecord(String skill, String level)
        {
          try
            {
                ClickAddNew();

                EnterSkill(skill);

                SelectLevel(level);

                ClickAdd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public bool VerifyMaxLimitOfRecords(int limit)
        {
            try
            {

                if (tableRows.Count == limit)
                {
                    CleanupSkillsRecords();
                    return true;
                }
                else
                CleanupSkillsRecords();
                return false;

            }
            catch (Exception e)
            {
                CleanupSkillsRecords();
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public void CleanupSkillRecords()
        {

            try
            {

                if (IsElementPresent(driver, tbodyLocator))
                {
                    ReadOnlyCollection<IWebElement> tableRows = GetTableRows();
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
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }

        }
    }
}
