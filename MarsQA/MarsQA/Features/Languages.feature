Feature: Languages for a user profile 
	
@mytag
Scenario: Verify user is able to add new Language to the profile
	Given the user logs into Porject Mars 
    And the user navigate to Languages under the profile tab
    When the user add a new Language 
	Then Verify Language record is created 