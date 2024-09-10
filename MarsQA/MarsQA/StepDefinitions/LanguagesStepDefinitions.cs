using MarsQA.Drivers;
using MarsQA.SpecflowPages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace MarsQA
{
    [Binding]
    public class LanguagesStepDefinition 
    {
        LoginPage loginPageObj = new LoginPage();
        HomePage homePageObj = new HomePage();
        LanguagesPage languagObj = new LanguagesPage();



        [Given(@"the user logs into Porject Mars")]
        public void GivenTheUserLogsIntoPorjectMars()
        {
            loginPageObj.LoginSteps();

        }

        [Given(@"the user navigate to Languages under the profile tab")]
        public void GivenTheUserNavigateToLanguagesUnderTheProfileTab()
        {
            homePageObj.GoToProfileTab();
        }

        [When(@"the user add a new Language")]
        public void WhenTheUserAddANewLanguage()
        {
            languagObj.CreateLanguageRecord();

        }

        [Then(@"Verify Language record is created")]
        public void ThenVerifyLanguageRecordIsCreated()
        {
           String actualLanguageText = languagObj.VerifyLanguageRecordCreated("Telugu");
            Assert.AreEqual("Telugu", actualLanguageText, "The text of the element is not as expected.");

        }

        [AfterScenario(Order = 0)]
        public void CleanupLanguages() {

            languagObj.CleanupLanguageRecords();
         
        }
    }
}
