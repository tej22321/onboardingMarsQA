Feature: Adding Languages to a user profile 
	
	Background: 
	 
	  Given user logs in to Project Mars 
	  And navigate to Languages under profile tab


	@addLanguage
	Scenario Outline: Add new Language to user profile
	 When user add '<Language>' and '<Level>'
	 Then Verify Language record is created
	  
	 
	 Examples: 

	 | Language   | Level          |
	 | Telugu     | Fluent         | #TC001 Authorised person, trying to add a new language to the profile
	 | spacebar   | Basic          | #TC012 Add a language with invalid input,  only white space
	 | 12345      | Conversational | #TC007 Add a language with invalid input, numeric data
	 | @#$$%^     | Basic          | #TC010 Add a language with invalid input,  special characters
	 |            | Conversational | #TC014 Add a language with invalid input,  empty field
	 | English $% | Basic          | #TC016 Add a language with invalid input, alphanumeric characters
	 | hugestring | Basic          |

	 @checkDuplicate
  Scenario: Verify duplicate Language record is not allowed for "Telugu Fluent"
  #TC018 Add a language with valid input, exisiting data
    When user adds "Telugu" and "Fluent"  
	And user adds "Telugu" and "Fluent" again
    Then Verify duplicate Language record is not allowed

	@checkCaseSensitivity
	  Scenario: Verify case sentive same Language record is not allowed for "Telugu Fluent"
  #TC020 Add a language with invalid input, exisiting data with case sensitivity
    When user adds "Telugu" and "Fluent"  
	And user adds "telugu" and "Fluent" again
    Then Verify duplicate Language record with case sensitivity is not allowed

	
	@editLanguageRecord
	  Scenario: Edit existing language record
  #TC003  Edit existing language in the profile.	
    When user adds "Telugu" and "Fluent"  
	And user make changes to the exisiting "Telugu" "Fluent" record and update text "Teluguu"
    Then Verify updated language record is saved 

	@removeLanguageRecord
 Scenario: Remove language record
  #TC005 Remove the  language in the profile
    When user adds "Telugu" and "Fluent"  
	And user navigate to "Telugu" "Fluent" record and remove the record
    Then Verify  language record is removed 

	@MaximumlimitofLanguageRecords
	Scenario: Maximum limit of Language Records
  #TC009 Verify the maximum limit for languages in the profile.
     When user adds the following languages:
    | Language   | Level          |
    | Telugu     | Basic          |
    | telugu     | Fluent         |
    | Hindi      | Conversational |
    | Tamil      | Fluent         |
    | Spanish    | Fluent         |
	Then Verify only "4"  record are saved

