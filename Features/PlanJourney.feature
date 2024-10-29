Feature: PlanJourney

@PlanJourney

Scenario: Verify a valid journey can be planned using widget
	Given I am on TFL Homepageto plan a journey
	And I enter source 'Leicester Square' and destination 'Covent Garden'
	When I submit my details
	Then I validate cycling time is 1 mins and walking time is 6 mins

Scenario:Verify journey with least walking by updating preference
	Given I am on journey result page
	When I edit my prefrence with least walking journey
	Then I validate my updated walking time is 11 

Scenario: Verify complete access information at Covent Garden
	Given I am on journey result page
	When I edit my prefrence with least walking journey
	Then I click on View Details and verify the information

Scenario: Verify widget with invalid input
	Given I am on TFL Homepageto plan a journey
	And I validate result for invalid source 'Invalid Location Test' and valid destination 'Covent Garden'
	Given I am on TFL Homepageto plan a journey
	Then I validate result for invalid destination 'Invalid Location Test' and valid source 'Leicester Square'

Scenario: Verify widget when location is blank
	Given I am on TFL Homepageto plan a journey
	Then I verify widget does not plan journey when location is blank

#@AdditionalTests

#Scenario:Verify 'Update journey' preferences are saved for future visits
	#Given I am on the journey result page
	#When I edit my preference with a maximum walking time of 30 minutes
	#And I select "Save these preferences for future visits"
	#Then I validate that the preference is saved and applied when I return to the journey planning page

#Scenario: Verify 'Routes with fewest changes' preference
    #Given I am on the journey result page
    #When I edit my preference to 'Routes with fewest changes'
    #Then I validate that the journey results prioritize routes with minimal transfers

#Scenario: Verify journey planning with departure time preference
    #Given I am on the TFL Homepage to plan a journey
    #And I enter source 'Leicester Square' and destination 'Covent Garden'
    #When I set a future departure time (e.g. tomorrow at 10:00 AM)
    #Then I validate that the journey is planned for the selected day and time



