using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsQA.Configuration;
using MarsQA.Drivers;
using MarsQA.SpecflowPages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;


namespace MarsQA.Hooks
{
    [Binding]
    public class Hooks
    {
        
        private readonly CommonDriver commonDriver;
        private readonly ScenarioContext scenarioContext;
        private IWebDriver driver;

        public Hooks(CommonDriver commonDriver, ScenarioContext sceanrioContext) { 
        
            this.commonDriver = commonDriver;
            this.scenarioContext = sceanrioContext;
                    
            
        }
       

        //[BeforeTestRun]
        public static void CleanUpBeforeTests()
        {
            // Initialize the WebDriver before the test run
            CommonDriver commonDriver = new CommonDriver();
            commonDriver.InitializeDriver("chrome");

             IWebDriver driver = commonDriver.Driver;
            driver.Navigate().GoToUrl(Config.Url);
            
            LoginPage loginPageObject = new LoginPage(commonDriver);
            

            loginPageObject.ClickSingIn(); // click on singIn button
            loginPageObject.EnterUserName(Config.Username); // Enter the username form the config file 
            loginPageObject.EnterPassword(Config.Password); // Enter password from the config file 
            loginPageObject.ClickLogin(); // clikc on the login button 
            loginPageObject.ClickProfileTab();

          // test run clear all the languages in the table
            LanguagesPage languagesPage = new LanguagesPage(commonDriver);
            languagesPage.NavigateToLanguageTab();
            languagesPage.CleanupLanguageRecords(); 
            
            

            // test run will clear all the skills in the table 
            SkillsPage skillsPage = new SkillsPage(commonDriver);
            skillsPage.NavigateToSkillsTab();
            skillsPage.CleanupSkillsRecords();


            loginPageObject.ClickSingOut();
            commonDriver.CloseDriver();


        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Initialize the WebDriver before each scenario
            
            
            commonDriver.InitializeDriver("chrome");
            
             this.driver = commonDriver.Driver;
                              

            driver.Navigate().GoToUrl(Config.Url);
        }

        [AfterScenario]
        public void AfterScenario()
        {
          //  LanguagesPage languagesPage = new LanguagesPage(commonDriver);
            //   languagesPage.CleanupLanguageRecords();
            
          //   languagesPage.DeleteLanguageEntry();
            
            commonDriver.CloseDriver();
        }
    }

}
