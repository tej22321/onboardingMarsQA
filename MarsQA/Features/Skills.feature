@SkillsFeature
Feature: Adding Skills to a user profile 
	
	Background: 
	 
	  Given user logs in to Project Mars 
	  And navigate to Skills under profile tab


	@addSkills
	Scenario Outline: Add new Skills to user profile
	 When user add '<Skills>' and '<Level>' records
	 Then Verify Skills record is created
	  
	 
	 Examples: 

	 | Skills     | Level                 |
	 | Java	      | Beginner              | #TC002 Authorised person, trying to add a new skills to the profile  
	 | spacebar   | Intermediate          | #TC013  Add a skill with invalid input,  only white space 
	 | 12345      | Beginner              | #TC008 Add a skill with invalid input, numeric data
	 | @#$$%^     | Expert                | #TC011 Add a skill with invalid input,  special characters 
	 |            | Expert                | #TC015 Add a Skill with invalid input,  empty field
	 | C $%       | Intermediate          | #TC017 Add a Skill with invalid input, alphanumeric characters 
	 
	  @checkDuplicate
  Scenario: Verify duplicate Skills record is not allowed for "Java Beginner"
  #TC019 Add a Skill with valid input, exisiting data
    When user adds "Java" and "Beginner" records
	And user adds "Java" and "Beginner" again to skills
    Then Verify duplicate Skills record is not allowed

	@checkCaseSensitivity
	  Scenario: Verify case sentive same Skill record is not allowed for "Java Beginner"
  #TC020 Add a language with invalid input, exisiting data with case sensitivity
    When user adds "Java" and "Beginner" records 
	And user adds "java" and "Beginner" again to skills
    Then Verify duplicate Skills record with case sensitivity is not allowed

	@editSkillRecord
	  Scenario: Edit existing skill record
  #TC004  Edit existing skill in the profile.	
    When user adds "Java" and "Beginner" records 
	And user make changes to the exisiting "Java" "Beginner" record and update text to "JAVAA" 
    Then Verify updated skill record is saved 

	@removeSkillRecord
 Scenario: Remove Skill record
  #TC006 Remove the  Skill record in the profile
    When user adds "Java" and "Beginner" records
	And user navigate to "Java" "Beginner" record and remove it
    Then Verify  Skill record is removed 

	