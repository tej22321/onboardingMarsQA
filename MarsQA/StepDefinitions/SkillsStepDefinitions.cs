using MarsQA.Configuration;
using MarsQA.SpecflowPages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using TechTalk.SpecFlow;

namespace MarsQA.StepDefinitions
{

    [Binding]
    public class SkillsStepDefinitions
    {

        private readonly LoginPage loginPageObject;
        private readonly SkillsPage skillsPageObject;
        public SkillsStepDefinitions(LoginPage loginpage, SkillsPage skillspage) { 
         this.loginPageObject = loginpage;
            this.skillsPageObject = skillspage;
        }

              
        [Given(@"navigate to Skills under profile tab")]
        public void GivenNavigateToSkillsUnderProfileTab()
        {
            loginPageObject.ClickProfileTab();
            skillsPageObject.NavigateToSkillsTab();
        }

        
        [When(@"user add '([^']*)' and '([^']*)' records")]
        public void WhenUserAddAndRecords(string Skills, string Level)
        {
            //click on add new button
            skillsPageObject.ClickAddNew();

            //Enter the value to language textbox
            if (Skills == "spacebar")
            {
                Skills = "    ";
                skillsPageObject.EnterSkill(Skills);
            }
            else
            {
                String SkillTrim = Skills.Trim();
                if (String.IsNullOrEmpty(SkillTrim))
                {
                    Console.WriteLine("Empty fileds are not allowed");
                }
                skillsPageObject.EnterSkill(Skills);

            }

            //select the value of language level
            skillsPageObject.SelectLevel(Level);

            //click on add button
            skillsPageObject.ClickAdd();

        }

        [Then(@"Verify Skills record is created")]
        public void ThenVerifySkillsRecordIsCreated()
        {
            Boolean IsRecordPresent = skillsPageObject.VerifySkillRecordCreated();
            Assert.IsTrue(IsRecordPresent, "Skill record is created which shouldnt be");

        }

        [When(@"user adds ""([^""]*)"" and ""([^""]*)"" records")]
        public void WhenUserAddsAndRecords(string skill, string level)
        {
            //click on add new button
            skillsPageObject.ClickAddNew();

            //Enter the value to language textbox
            skillsPageObject.EnterSkill(skill);

            //select the value of language level
            skillsPageObject.SelectLevel(level);

            //click on add button
            skillsPageObject.ClickAdd();
        }

        [When(@"user adds ""([^""]*)"" and ""([^""]*)"" again to skills")]
        public void WhenUserAddsAndAgainToSkills(string skill, string level)
        {

            //click on add new button
            skillsPageObject.ClickAddNew();

            //Enter the value to language textbox
            skillsPageObject.EnterSkill(skill);

            //select the value of language level
            skillsPageObject.SelectLevel(level);

            //click on add button
            skillsPageObject.ClickAdd();
        }

        [Then(@"Verify duplicate Skills record is not allowed")]
        public void ThenVerifyDuplicateSkillsRecordIsNotAllowed()
        {
            Boolean IsErrorMesssagePresent = skillsPageObject.IsErrorMessageExists();
            Console.WriteLine(IsErrorMesssagePresent);
            Assert.True(IsErrorMesssagePresent, "No error message was found");
        }

        [Then(@"Verify duplicate Skills record with case sensitivity is not allowed")]
        public void ThenVerifyDuplicateSkillsRecordWithCaseSensitivityIsNotAllowed()
        {
            Boolean IsErrorMesssagePresent = skillsPageObject.IsErrorMessageExists();
            Assert.True(IsErrorMesssagePresent, "No Error message was found");
        }

        [When(@"user make changes to the exisiting ""([^""]*)"" ""([^""]*)"" record and update text to ""([^""]*)""")]
        public void WhenUserMakeChangesToTheExisitingRecordAndUpdateTextTo(string skill, string level, string updatedSKill)
        {
            skillsPageObject.UpdateSkillRecord(skill, level, updatedSKill);
        }

        [Then(@"Verify updated skill record is saved")]
        public void ThenVerifyUpdatedSkillRecordIsSaved()
        {
            Boolean isRecordUpdated = skillsPageObject.VerifyUpdatedSkillRecord();
            Assert.True(isRecordUpdated, "No record is not updated");
        }

        [When(@"user navigate to ""([^""]*)"" ""([^""]*)"" record and remove it")]
        public void WhenUserNavigateToRecordAndRemoveIt(string skill, string level)
        {
            skillsPageObject.removeSkillRecord(skill, level);
        }

        [Then(@"Verify  Skill record is removed")]
        public void ThenVerifySkillRecordIsRemoved()
        {
            Boolean isRecordPresent = skillsPageObject.VerifySkillRecordRemoved();
            Assert.True(isRecordPresent, "Record still exists in the Skills table");
        }

        [When(@"user adds the following skills:")]
        public void WhenUserAddsTheFollowingSkills(Table table)
        {
            
            foreach (var row in table.Rows)
            {
                
                String skills = row["Skills"];
                String level = row["Level"];

                skillsPageObject.AddSkillsRecord(skills, level);

            }
        }

       [Then(@"Verify ""([^""]*)"" record are saved")]
        public void ThenVerifyRecordAreSaved(int limit)
        {
            bool maxlimit = skillsPageObject.VerifyMaxLimitOfRecords(limit);
            Assert.True(maxlimit, "exceeded the limit of records");

        }

    }
}
