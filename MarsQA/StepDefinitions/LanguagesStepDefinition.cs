using MarsQA.SpecflowPages;
using MarsQA.Configuration;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace MarsQA.StepDefinitions
{
    [Binding]
    public class LanguagesStepDefinition
    {
        private readonly LoginPage loginPageObject;
        private readonly LanguagesPage languagesPageObject;

      public  LanguagesStepDefinition(LoginPage loginpage, LanguagesPage languagespage)
        {
            this.loginPageObject = loginpage;
            this.languagesPageObject = languagespage;
        }

        [Given(@"user logs in to Project Mars")]
        public void GivenUserLogsInToProjectMars()
        {
            //click on sign in  button on the app 
            
            loginPageObject.ClickSingIn();     
            loginPageObject.EnterUserName(Config.Username); // Enter the username form the config file 
            loginPageObject.EnterPassword(Config.Password); // Enter password from the config file 
            loginPageObject.ClickLogin(); // clikc on the login button 
        }

        [Given(@"navigate to Languages under profile tab")]
        public void GivenNavigateToLanguagesUnderProfileTab()
        {
            loginPageObject.ClickProfileTab();
            languagesPageObject.NavigateToLanguageTab();
        }

        [When(@"user add '([^']*)' and '([^']*)'")]
        public void WhenUserAddAnd(string Language, string Level)
        {
            //click on add new button
            languagesPageObject.ClickAddNew();

            //Enter the value to language textbox
            if (Language == "spacebar")
            {
                Language = "    ";
                languagesPageObject.EnterLangugage(Language);
            }

            if(Language == "hugestring")
            {
                Language = new string('A', 10000);
                languagesPageObject.EnterLangugage(Language);
            }
            else
            {
                String languageTrim = Language.Trim();
                if(String.IsNullOrEmpty(languageTrim))
                {
                    Console.WriteLine("Empty fileds are not allowed");
                }
                languagesPageObject.EnterLangugage(Language);

            }

            //select the value of language level
            languagesPageObject.SelectLevel(Level);

            //click on add button
            languagesPageObject.ClickAdd();

        }

        [Then(@"Verify Language record is created")]
        public void ThenVerifyLanguageRecordIsCreated()
        {
            Boolean IsRecordPresent = languagesPageObject.VerifyLanguageRecordCreated();
           Assert.IsTrue(IsRecordPresent,"language record is created which shouldnt be");

            //Assert.IsFalse(IsRecordPresent, "record is created, which should'nt be");
            
        }

        // code to check duplicate record 
        [When(@"user adds ""([^""]*)"" and ""([^""]*)""")]
        public void WhenUserAddsAnd(string language, string level)
        {
            //click on add new button
            languagesPageObject.ClickAddNew();

            //Enter the value to language textbox
            languagesPageObject.EnterLangugage(language);

            //select the value of language level
            languagesPageObject.SelectLevel(level);

            //click on add button
            languagesPageObject.ClickAdd();

        }

        [When(@"user adds ""([^""]*)"" and ""([^""]*)"" again")]
        public void WhenUserAddsAndAgain(string language, string level)
        {
            //click on add new button
            languagesPageObject.ClickAddNew();

            //Enter the value to language textbox
            languagesPageObject.EnterLangugage(language);

            //select the value of language level
            languagesPageObject.SelectLevel(level);

            //click on add button
            languagesPageObject.ClickAdd();
        }

        [Then(@"Verify duplicate Language record is not allowed")]
        public void ThenVerifyDuplicateLanguageRecordIsNotAllowed()
        {
         Boolean IsErrorMesssagePresent =   languagesPageObject.IsErrorMessageExists();
            Console.WriteLine(IsErrorMesssagePresent);
            Assert.True(IsErrorMesssagePresent, "No error message was found");
        }

        [Then(@"Verify duplicate Language record with case sensitivity is not allowed")]
        public void ThenVerifyDuplicateLanguageRecordWithCaseSensitivityIsNotAllowed()
        {
            Boolean IsErrorMesssagePresent =   languagesPageObject.IsErrorMessageExists();
            Assert.True(IsErrorMesssagePresent, "No Error message was found");
        }


        [When(@"user make changes to the exisiting ""([^""]*)"" ""([^""]*)"" record and update text ""([^""]*)""")]
        public void WhenUserMakeChangesToTheExisitingRecordAndUpdateText(string languageElementValue, string languageConversationalLevel, string updateLanguageElementValue)
        {
            languagesPageObject.UpdateLanguageRecord(languageElementValue, languageConversationalLevel, updateLanguageElementValue);
        }

        [Then(@"Verify updated language record is saved")]
        public void ThenVerifyUpdatedLanguageRecordIsSaved()
        
        {
           Boolean isRecordUpdated =  languagesPageObject.VerifyUpdatedLanguageRecord();
            Assert.True(isRecordUpdated,"No record is not updated");
        }


        [When(@"user navigate to ""([^""]*)"" ""([^""]*)"" record and remove the record")]
        public void WhenUserNavigateToRecordAndRemoveTheRecord(string language, string level)
        {
            languagesPageObject.removeLanguageRecord(language, level);
        }

        [Then(@"Verify  language record is removed")]
        public void ThenVerifyLanguageRecordIsRemoved()
        {
          Boolean isRecordPresent =   languagesPageObject.VerifyLanguageRecordRemoved();
            Assert.True(isRecordPresent, "Record still exists in the language table");
        }


        [When(@"user adds the following languages:")]
        public void WhenUserAddsTheFollowingLanguages(Table table)
        {
            int rowcount = 0;
           foreach(var row in table.Rows)
            {
                rowcount++;
                String language = row["Language"];
                String level = row["Level"];
                
               languagesPageObject.AddLanguageRecord(language, level,rowcount);
                                
              }
        }

        [Then(@"Verify only ""([^""]*)""  record are saved")]
        public void ThenVerifyOnlyRecordAreSaved(int limit)
        {
            bool maxlimit = languagesPageObject.VerifyMaxLimitOfRecords(limit);
            Assert.True(maxlimit, "exceeded the limit of records");
           // Assert.AreEqual(maxlimit, limit, "exceeded te limit");

        }

    }
}
